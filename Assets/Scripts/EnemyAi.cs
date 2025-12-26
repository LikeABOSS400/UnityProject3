using UnityEngine;
using System.Collections.Generic;
public class EnemyAi : MonoBehaviour
{
    public GridMap grid;
    public Transform target;

    private AStar aStar;
    private List<Vector2Int> path;
    private int pathIndex = 0;

    private float repathTimer = 0f;

    void Start()
    {
        aStar = new AStar(grid);
    }


    void Update()
    {
        repathTimer += Time.deltaTime;

        if (repathTimer >= 0.25f)
        {
            Vector2Int start = WorldToGrid(transform.position);
            Vector2Int end = WorldToGrid(target.position);

            path = aStar.FindPath(start, end);
            pathIndex = 0;

            repathTimer = 0f;
        }

        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (path == null || pathIndex >= path.Count) return;

        Vector3 targetPos = GridToWorld(path[pathIndex]);
        float speed = 3f;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            pathIndex++;
        }
        
    }

    public Vector2Int WorldToGrid(Vector3 world)
    {
        return new Vector2Int(
            Mathf.FloorToInt(world.x / grid.cellSize),
            Mathf.FloorToInt(world.y / grid.cellSize)
            );
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * grid.cellSize + grid.cellSize * 0.5f,
            gridPos.y * grid.cellSize + grid.cellSize * 0.5f,
            0
            );
    }
}
