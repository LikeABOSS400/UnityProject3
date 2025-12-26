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

    private static List<Sector> allSectors = new List<Sector>();
    [SerializeField] private GameObject Highlight;

    private void Awake()
    {
        allSectors.Add(this);
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
        SceneManager.LoadScene("SampleScene");
        ApplyChaos();
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
    }

    public void AddChaos(float value)
    {
        chaos += value;
        chaos = Mathf.Clamp(chaos, 0, 4);
        if (chaosMeter != null)
        {
            chaosMeter.UpdateChaos(chaos);
        }
    }
}
