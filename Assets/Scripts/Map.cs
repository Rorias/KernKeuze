using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap collisionTM;

    public int width;
    public int height;

    public Cell[,] map;

    private void Start()
    {
        map = new Cell[width, height];
        map.Initialize();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new Cell(new Vector2Int(x, y), collisionTM);
            }
        }
    }
}
