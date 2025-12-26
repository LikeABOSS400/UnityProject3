using UnityEngine;

public class GridMap : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize = 1f;

    public bool[,] walkableMap;

    public void Init(bool[,] map)
    {
        walkableMap = map;
        width = map.GetLength(0);
        height = map.GetLength(1);
    }

    public bool IsWalkable(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return false;
        }
        else
        {
            return walkableMap[x, y];
        }
    }
}
