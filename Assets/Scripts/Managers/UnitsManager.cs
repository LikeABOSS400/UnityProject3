using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
public class UnitsManager : MonoBehaviour
{
    public static UnitsManager Instance;
    [SerializeField] private string Sector = "District_select";

    private int enemyCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CountEnemy()
    {
        enemyCount++;
    }

    public void DefeatEnemy()
    {
        enemyCount = Mathf.Max(0, enemyCount - 1);
        if(enemyCount <= 0)
        {
            SceneManager.LoadScene(Sector);
        }
    }
}
