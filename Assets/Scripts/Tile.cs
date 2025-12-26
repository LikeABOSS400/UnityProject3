using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
public abstract class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private Boolean isWalkable;

    public BaseUnit OccupiedUnit;
    public Boolean IsWalkable => isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }


    void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.PlayerTurn) return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.player)
            {
                UnitsManager.Instance.SetSelectedPlayer((BasePlayer)OccupiedUnit);
            }
            else
            {
                if (UnitsManager.Instance.selectedPlayer != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitsManager.Instance.SetSelectedPlayer(null);
                }
            }
        }
        else
        {
            if(UnitsManager.Instance.selectedPlayer != null)
            {
                SetUnit(UnitsManager.Instance.selectedPlayer);
                UnitsManager.Instance.SetSelectedPlayer(null);

                GameManager.Instance.ChangeState(GameState.EnemyTurn);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if(unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
