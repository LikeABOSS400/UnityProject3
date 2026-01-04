using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Diagnostics.Tracing;
public class Sector : MonoBehaviour
{
    public ChaosManager chaosMeter;
    public float chaos = 0;
    private string sectorID;

    private static List<Sector> allSectors = new List<Sector>();
    [SerializeField] private GameObject Highlight;

    private void Awake()
    {
        allSectors.Add(this);

        sectorID = gameObject.name;
        if (DataManager.Instance != null)
        {
            chaos = DataManager.Instance.LoadChaos(sectorID, chaos);
        }

        if (chaosMeter != null)
        {
            chaosMeter.UpdateChaos(chaos);
        }
    }
    private void OnMouseEnter()
    {
        Highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        Highlight.SetActive(false);
    }
    public void OnMouseDown()
    {
        ApplyChaos();
        SceneManager.LoadScene("SampleScene");

    }

    public void ApplyChaos()
    {
        detractChaos(1);

        List<Sector> pool = new List<Sector>(allSectors);
        pool.Remove(this);

        if(pool.Count == 0)
        {
            return;
        }

        int count = UnityEngine.Random.Range(1, Mathf.Min(3, pool.Count));

        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);

            Sector sector = pool[index];

            sector.AddChaos(1);
        }
    }

    private void OnDestroy()
    {
        allSectors.Remove(this);
    }

    public void detractChaos(float value)
    {
        chaos -= value;
        chaos = Mathf.Clamp(chaos, 0, 4);
        if (chaosMeter != null)
        {
            chaosMeter.UpdateChaos(chaos);
        }

        if(DataManager.Instance != null)
        {
            DataManager.Instance.SaveChaos(sectorID, chaos);
        }
    }

    public void AddChaos(float value)
    {
        chaos += value;
        chaos = Mathf.Clamp(chaos, 0, 4);
        if (chaosMeter != null)
        {
            chaosMeter.UpdateChaos(chaos);
        }

        if(DataManager.Instance != null)
        {
            DataManager.Instance.SaveChaos(sectorID, chaos);
        }
    }
}
