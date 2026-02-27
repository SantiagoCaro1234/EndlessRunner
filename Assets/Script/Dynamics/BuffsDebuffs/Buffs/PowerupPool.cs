using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public PowerUpData powerUpData;
    public int initialPoolSize = 3;

    private Stack<PowerUp> pool;

    private void Awake()
    {
        pool = new Stack<PowerUp>();
        for (int i = 0; i < initialPoolSize; i++)
            CreateNew();
    }

    private PowerUp CreateNew()
    {
        GameObject go = Instantiate(powerUpData.prefab);
        go.SetActive(false);
        PowerUp pu = go.GetComponent<PowerUp>();
        if (pu == null)
            pu = go.AddComponent<PowerUp>();
        pu.Init(powerUpData, this);
        pool.Push(pu);
        return pu;
    }

    public PowerUp Get(Vector3 position, Quaternion rotation)
    {
        PowerUp pu;
        if (pool.Count > 0)
        {
            pu = pool.Pop();
            pu.transform.position = position;
            pu.transform.rotation = rotation;
            pu.gameObject.SetActive(true);
        }
        else
        {
            pu = CreateNew();
            pu.transform.position = position;
            pu.transform.rotation = rotation;
            pu.gameObject.SetActive(true);
        }
        return pu;
    }

    public void Return(PowerUp pu)
    {
        pu.gameObject.SetActive(false);
        pool.Push(pu);
    }
}