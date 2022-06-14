using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    private WayPoint localWayPoint;

    public void Spawn()
    {
        GameObject enemy = Instantiate(spawnable, transform);
        enemy.GetComponent<Enemy>().SetLocalSpawnPoint(this);
    }

    public void SetLocalWayPoint(WayPoint localWayPoint)
    {
        this.localWayPoint = localWayPoint;
    }

    public void LocalEnemyDead()
    {
        localWayPoint.EnemyDead();
    }
}
