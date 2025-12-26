using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject selected, tileInfo, tileUnit;

    void Awake()
    {
        Instance = this;
    }

    public void ShowTileInfo(Tile tile)
    {
        if(tile == null)
        {
            tileInfo.SetActive(false);
            tileUnit.SetActive(false);
            return;
        }

        tileInfo.GetComponentInChildren<TMP_Text>().text = tile.TileName;
        tileInfo.SetActive(true);

        if(tile.OccupiedUnit)
        {
            tileUnit.GetComponentInChildren<TMP_Text>().text = tile.OccupiedUnit.unitName;
            tileUnit.SetActive(true);
        }
    }
    public void ShowSelectedUnit(BasePlayer unit)
    {
        if (unit == null)
        {
            selected.SetActive(false);
            return;
        }

        selected.GetComponentInChildren<TMP_Text>().text = unit.unitName;
        selected.SetActive(true);
    }
}
