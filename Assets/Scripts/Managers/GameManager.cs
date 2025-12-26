using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch(newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnUnits:
                UnitsManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitsManager.Instance.SpawnEnemies();
                ChangeState(GameState.PlayerTurn);
                break;
            case GameState.PlayerTurn:
                Debug.Log("Player turn");
                break;
            case GameState.EnemyTurn:
                Debug.Log("Enemy turn");
                StartCoroutine(EnemyTurnRoutine());
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);


        }
    }
    private IEnumerator EnemyTurnRoutine()
    {
        foreach(BaseEnemy enemy in UnitsManager.Instance.enemies)
        {
            enemy.TakeTurn();
            yield return new WaitForSeconds(0f);
        }

        ChangeState(GameState.PlayerTurn);
    }

}



public enum GameState
{
    GenerateGrid = 0,
    PlayerTurn = 1,
    EnemyTurn = 2,
    SpawnUnits = 3,
    SpawnEnemies = 4
}

