using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar
{
    #region Singleton
    private static AStar _instance;
    private static object _lock = new object();

    private AStar() { }

    public static AStar Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (_lock)
                {
                    if (null == _instance)
                    {
                        _instance = new AStar();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion

    public Tilemap collTM;

    public List<Vector2> FindPathToTarget(Vector2Int _startPos, Vector2Int _endPos, Cell[,] _map)
    {
        List<Vector2> posList = new List<Vector2>();

        Cell startCell = _map[_startPos.x, _startPos.y];
        Cell targetCell = _map[_endPos.x, _endPos.y];

        if (targetCell.blocked)
        {
            return posList;
        }

        startCell.GScore = 0;
        startCell.HScore = CalculateDistanceCost(startCell, targetCell);

        List<Cell> openList = new List<Cell>();
        HashSet<Cell> closedList = new HashSet<Cell>();

        openList.Add(startCell);

        while (openList.Count > 0)
        {
            Cell currentCell = openList[0];

            for (int i = 0; i < openList.Count; i++)
            {
                //add the next cell to the list only if it's f score is smaller than the current cell, or if it's f score is equal but h score is lower
                if (openList[i].FScore < currentCell.FScore || (openList[i].FScore == currentCell.FScore && openList[i].HScore < currentCell.HScore))
                {
                    currentCell = openList[i];
                }
            }

            openList.Remove(currentCell);
            closedList.Add(currentCell);

            if (currentCell.position == targetCell.position)
            {
                posList = GetFinalPath(startCell, targetCell);
                break;
            }

            foreach (Cell neighbour in GetAvailableNeighbours(currentCell, _map, closedList))
            {
                int newGScore = currentCell.GScore + CalculateDistanceCost(currentCell, neighbour);

                if (newGScore < neighbour.GScore)
                {
                    neighbour.parent = currentCell;
                    neighbour.GScore = newGScore;
                    neighbour.HScore = CalculateDistanceCost(neighbour, targetCell);

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        ResetMapScore(openList, closedList);

        return posList;
    }

    private void ResetMapScore(List<Cell> openList, HashSet<Cell> closedList)
    {
        foreach (Cell cell in openList)
        {
            cell.GScore = int.MaxValue;
        }

        foreach (Cell cell in closedList)
        {
            cell.GScore = int.MaxValue;
        }
    }

    private List<Vector2> GetFinalPath(Cell _startCell, Cell _endCell)
    {
        List<Vector2> finalPath = new List<Vector2>();
        Cell currentCell = _endCell;

        while (currentCell != _startCell)
        {
            finalPath.Add(new Vector2(currentCell.position.x + 0.5f, currentCell.position.y + 0.5f));
            currentCell = currentCell.parent;
        }

        finalPath.Reverse();

        return finalPath;
    }

    private List<Cell> GetAvailableNeighbours(Cell _cell, Cell[,] _map, HashSet<Cell> _closed)
    {
        List<Cell> neighbours = new List<Cell>();

        if (_cell.position.y + 1 < _map.GetLength(1) && !_cell.IsBlocked(_map[_cell.position.x, _cell.position.y + 1]))
        {
            neighbours.Add(_map[_cell.position.x, _cell.position.y + 1]);
        }

        if (_cell.position.x + 1 < _map.GetLength(0) && !_cell.IsBlocked(_map[_cell.position.x + 1, _cell.position.y]))
        {
            neighbours.Add(_map[_cell.position.x + 1, _cell.position.y]);
        }

        if (_cell.position.y - 1 > 0 && !_cell.IsBlocked(_map[_cell.position.x, _cell.position.y - 1]))
        {
            neighbours.Add(_map[_cell.position.x, _cell.position.y - 1]);
        }

        if (_cell.position.x - 1 > 0 && !_cell.IsBlocked(_map[_cell.position.x - 1, _cell.position.y]))
        {
            neighbours.Add(_map[_cell.position.x - 1, _cell.position.y]);
        }

        for (int i = 0; i < neighbours.Count;)
        {
            if (_closed.Any(x => x.position == neighbours[i].position))
            {
                neighbours.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        return neighbours;
    }

    private int CalculateDistanceCost(Cell _a, Cell _b)
    {
        int xDistance = Mathf.Abs(_a.position.x - _b.position.x);
        int yDistance = Mathf.Abs(_a.position.y - _b.position.y);

        return xDistance + yDistance;
    }
}
