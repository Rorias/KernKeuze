using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class Cell
{
    public Vector2Int position;
    public Cell parent;
    public bool blocked;

    public int FScore
    {
        get { return GScore + HScore; }
    }

    public int GScore;
    public int HScore;

    public Cell(Vector2Int _position, Tilemap _coll) 
    { 
        position = _position;
        blocked = _coll.HasTile(_coll.WorldToCell(Vector2IntToVector3(position)));
        GScore = int.MaxValue;
    }

    public bool IsBlocked(Cell neighbour)
    {
        return neighbour.blocked;
    }

    private Vector3 Vector2IntToVector3(Vector2Int pos)
    {
        return new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
    }
}
