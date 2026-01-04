using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public Dictionary<string, float> chaosValues = new Dictionary<string, float>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveChaos(string id, float value)
    {
        chaosValues[id] = value;
    }

    public float LoadChaos(string id, float defaultValue = 0)
    {
        if (chaosValues.TryGetValue(id, out float val))
            return val;

        return defaultValue;
    }
}
