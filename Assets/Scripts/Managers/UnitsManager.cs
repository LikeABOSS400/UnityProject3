using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance;
    private List<ScriptableUnits> units;
    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BasePlayer> players = new List<BasePlayer>();

    public BasePlayer selectedPlayer;
    private void Awake()
    {
        Instance = this;

        units = Resources.LoadAll<ScriptableUnits>("Units").ToList();
    }
    public void SpawnHeroes()
    {
        var heroCount = 1;

        for(int i = 0; i<heroCount;i++)
        {
            var randomPrefab = GetRandomUnit<BasePlayer>(Faction.player);
            var spawnedPlayer = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetPlayerSpawnTile();

            players.Add(spawnedPlayer);
            randomSpawnTile.SetUnit(spawnedPlayer);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            enemies.Add(spawnedEnemy);
            randomSpawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => UnityEngine.Random.value).First().unitPrefab;
    }
    
    public void SetSelectedPlayer(BasePlayer player)
    {
        selectedPlayer = player;
        MenuManager.Instance.ShowSelectedUnit(player);
    }
}
