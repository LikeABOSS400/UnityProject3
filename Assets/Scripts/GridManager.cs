using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _grassTile, _mountainTile;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    //public GridMap gridMap;

    private void Awake()
    {
        Instance = this;
    }



    public void GenerateGrid()
    {
        //gridMap.walkableMap = new bool[width, height];
        //gridMap.width = width;
        //gridMap.height = height;
        _tiles = new Dictionary<Vector2, Tile>();

        for ( int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                var SpawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                SpawnedTile.name = $"Tile {x} {y}";

                SpawnedTile.Init(x,y);

               _tiles[new Vector2(x, y)] = SpawnedTile;

                //gridMap.walkableMap[x, y] = true;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        GameManager.Instance.ChangeState(GameState.SpawnUnits);
    }

    public Tile GetPlayerSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.IsWalkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.IsWalkable).OrderBy(t => Random.value).First().Value;
    }


    public Tile GetTileInPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
        
    }

    public List<Tile> GetNeighbors(Tile tile)
    {
        List<Tile> result = new List<Tile>();

        Vector2 pos = tile.transform.position;
        Vector2[] dirs = new Vector2[]
        {
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1)
        };

        foreach(var dir in dirs)
        {
            var n = GetTileInPosition(pos + dir);
            if (n != null) result.Add(n);
        }

        return result;
    }
}
