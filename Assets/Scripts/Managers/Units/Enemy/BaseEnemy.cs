using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public void TakeTurn()
    {
        if (UnitsManager.Instance.players.Count == 0) return;

        BasePlayer target = UnitsManager.Instance.players[0];
        if (target == null) return;

        Tile start = this.OccupiedTile;
        Tile end = target.OccupiedTile;

        var path = TileAStar.FindPath(start, end);

        if (path == null || path.Count < 2) return;

        Tile nextTile = path[1];

        if (nextTile.IsWalkable || nextTile == end)
        {
            nextTile.SetUnit(this);

        }
    }
}

