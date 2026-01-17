using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
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
        ChangeState(GameState.ChoseDistrict);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch(newState)
        {
            case GameState.ChoseDistrict:
                SceneManager.LoadScene("District_select");
                break;
            case GameState.Combat:
                SceneManager.LoadScene("SampleScene");
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);


        }
    }

}



public enum GameState
{
    ChoseDistrict = 0,
    Combat = 1
}

