using UnityEngine;
using System.Collections.Generic;

public class ObstaclePool : MonoBehaviour
{
    public GameObject prefab;
    public int initialPoolSize = 5;

    private Stack<Obstacle> pool;

    private void Awake()
    {
        pool = new Stack<Obstacle>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObstacle();
        }
    }

    private Obstacle CreateNewObstacle()
    {
        GameObject go = Instantiate(prefab);
        go.SetActive(false);
        Obstacle obs = go.GetComponent<Obstacle>();
        if (obs == null)
            obs = go.AddComponent<Obstacle>();
        obs.Init(this); // pasar owner
        pool.Push(obs);
        return obs;
    }

    public Obstacle GetObstacle(Vector3 position, Quaternion rotation)
    {
        Obstacle obs;
        if (pool.Count > 0)
        {
            obs = pool.Pop();
            obs.transform.position = position;
            obs.transform.rotation = rotation;
            obs.gameObject.SetActive(true);
        }
        else
        {
            obs = CreateNewObstacle();
            obs.transform.position = position;
            obs.transform.rotation = rotation;
            obs.gameObject.SetActive(true);
        }
        obs.OnSpawn(); // notificar que se ha spawnado
        return obs;
    }

    public void ReturnObstacle(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
        pool.Push(obstacle);
    }
}