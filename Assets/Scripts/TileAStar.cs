using UnityEngine;
using System.Collections.Generic;
public class TileAStar
{
    public static List<Tile> FindPath(Tile start, Tile end)
    {
        var open = new List<Tile>();
        var closed = new HashSet<Tile>();
        var cameFrom = new Dictionary<Tile, Tile>();

        var gScore = new Dictionary<Tile, int>();
        var fScore = new Dictionary<Tile, int>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);

        open.Add(start);

        while (open.Count > 0)
        {
            Tile current = open[0];
            foreach (var t in open)
                if (fScore.ContainsKey(t) && fScore[t] < fScore[current])
                    current = t;

            if (current == end)
                return ReconstructPath(cameFrom, current);

            open.Remove(current);
            closed.Add(current);

            foreach (var neighbor in GridManager.Instance.GetNeighbors(current))
            {
                if (closed.Contains(neighbor))
                    continue;

                if (!neighbor.IsWalkable && neighbor != end)
                    continue;

                int tentative = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;
                    fScore[neighbor] = tentative + Heuristic(neighbor, end);

                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        return null;
    }

    private static int Heuristic(Tile a, Tile b)
    {
        Vector2 aa = a.transform.position;
        Vector2 bb = b.transform.position;
        return Mathf.Abs((int)(aa.x - bb.x)) + Mathf.Abs((int)(aa.y - bb.y));
    }

    private static List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        var path = new List<Tile> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
