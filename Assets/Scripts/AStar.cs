using System.Collections;
using System.Collections.Generic;

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

    private int mapLengthX;
    private int mapLengthY;

    public List<Vector2> FindPathToTarget(Vector2Int _startPos, Vector2Int _endPos, ref Cell[,] _map)
    {
        List<Vector2> posList = new List<Vector2>();

        Cell startCell = _map[_startPos.x, _startPos.y];
        Cell targetCell = _map[_endPos.x, _endPos.y];

        if (targetCell.blocked)
        {
            return posList;
        }

        mapLengthX = _map.GetLength(0);
        mapLengthY = _map.GetLength(1);

        startCell.GScore = 0;
        startCell.HScore = CalculateDistanceCost(startCell, targetCell);

        List<Cell> openList = new List<Cell>();
        List<Cell> closedList = new List<Cell>();

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

    private void ResetMapScore(List<Cell> openList, List<Cell> closedList)
    {
        for (int i = 0; i < openList.Count; i++)
        {
            openList[i].GScore = int.MaxValue;
        }

        for (int i = 0; i < closedList.Count; i++)
        {
            closedList[i].GScore = int.MaxValue;
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

    private List<Cell> GetAvailableNeighbours(Cell _cell, Cell[,] _map, List<Cell> _closed)
    {
        List<Cell> neighbours = new List<Cell>();

        Cell current = _map[_cell.position.x, _cell.position.y + 1];

        if (_cell.position.y + 1 < mapLengthY && !_cell.IsBlocked(current))
        {
            neighbours.Add(current);
        }

        current = _map[_cell.position.x + 1, _cell.position.y];

        if (_cell.position.x + 1 < mapLengthX && !_cell.IsBlocked(current))
        {
            neighbours.Add(current);
        }

        current = _map[_cell.position.x, _cell.position.y - 1];

        if (_cell.position.y - 1 > 0 && !_cell.IsBlocked(current))
        {
            neighbours.Add(current);
        }

        current = _map[_cell.position.x - 1, _cell.position.y];

        if (_cell.position.x - 1 > 0 && !_cell.IsBlocked(current))
        {
            neighbours.Add(current);
        }

        for (int i = 0; i < neighbours.Count;)
        {
            bool found = false;

            for (int j = 0; j < _closed.Count; j++)
            {
                if (_closed[j].position == neighbours[i].position)
                {
                    neighbours.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                i++;
            }
        }

        return neighbours;
    }

    private int CalculateDistanceCost(Cell _a, Cell _b)
    {
        int xDistance = Abs(_a.position.x - _b.position.x);
        int yDistance = Abs(_a.position.y - _b.position.y);

        return xDistance + yDistance;
    }

    private int Abs(int _value)
    {
        if (_value >= 0) { return _value; }
        else { return -_value; }
    }
}
