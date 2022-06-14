using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private List<SpawnPoint> spawnPointsList = new List<SpawnPoint>();
    private int enemyCount = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPointsList.Add(transform.GetChild(i).gameObject.GetComponent<SpawnPoint>());
        }
        enemyCount = spawnPointsList.Count;
    }

    public void Spawn()
    {
        foreach (SpawnPoint spawnPoint in spawnPointsList)
        {
            spawnPoint.Spawn();
            spawnPoint.SetLocalWayPoint(this);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void EnemyDead()
    {
        enemyCount--;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}
