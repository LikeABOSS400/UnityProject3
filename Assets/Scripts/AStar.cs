using UnityEngine;
using System.Collections.Generic;
public class AStar
{
    private GridMap grid;

    private readonly static Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(0,1),
        new Vector2Int(0,-1)
    };

    public AStar(GridMap map)
    {
        this.grid = map;
    }

    private int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public List<Vector2Int> FindPath(Vector2Int a, Vector2Int b)
    {
        Node[,] nodes = new Node[grid.width, grid.height];

        for (int x = 0; x < grid.width; x++)
        {
            for(int y = 0; y < grid.height; y++)
            {
                nodes[x, y] = new Node(x, y, grid.IsWalkable(x, y));
            }
        }

        List<Node> open = new List<Node>();
        HashSet<Node> closed = new HashSet<Node>();

        Node startNode = nodes[a.x, a.y];
        Node targetNode = nodes[b.x, b.y];

        open.Add(startNode);

        while (open.Count > 0)
        {
            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost || (open[i].fCost == current.fCost && open[i].hCost < current.hCost))
                {
                    current = open[i];
                }
            }

            open.Remove(current);
            closed.Add(current);

            if(current == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach( var dir in directions)
            {
                int nx = current.x + dir.x;
                int ny = current.y + dir.y;

                if (!grid.IsWalkable(nx, ny)) continue;

                Node neighbor = nodes[nx,ny];
                if (closed.Contains(neighbor)) continue;

                int newCost = current.gCost + 1;

                if (!open.Contains(neighbor) || newCost < neighbor.gCost)
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = Heuristic(new Vector2Int(nx, ny), b);
                    neighbor.parent = current;

                    if (!open.Contains(neighbor))
                    {
                        open.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    private List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(new Vector2Int(current.x, current.y));
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
}
