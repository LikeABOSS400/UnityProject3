using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New unit", menuName = "Scriptable Unit")]
public class ScriptableUnits : ScriptableObject
{
    public Faction Faction;
    public BaseUnit unitPrefab;
}

public enum Faction
{
    enemy = 0,
    player = 1
}
