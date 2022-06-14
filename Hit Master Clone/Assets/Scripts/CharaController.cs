using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharaController : MonoBehaviour
{
    [SerializeField] GameObject wayPointsConteiner;
    [SerializeField] string bulletTag;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Animator animator;
    [SerializeField] SceneLogic sceneLogic;

    private NavMeshAgent agent;
    private List<WayPoint> wayPointsList = new List<WayPoint>();
    private int currentWayPoint = 0;
    private BulletPooler bulletPooler;

    private void Start()
    {
        bulletPooler = BulletPooler.Instance;
        agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < wayPointsConteiner.transform.childCount; i++)
        {
            wayPointsList.Add(wayPointsConteiner.transform.GetChild(i).gameObject.GetComponent<WayPoint>());
            wayPointsList[i].Spawn();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        CallForWayPoint();

        AnimationHandler();
    }

    private void AnimationHandler()
    {
        if (!animator) return;

        if (agent.remainingDistance > 0.1f)
            animator.SetBool("isRun", true);
        else
            animator.SetBool("isRun", false);
    }

    private void CallForWayPoint()
    {
        agent.SetDestination(wayPointsList[currentWayPoint].GetPosition());

        if (wayPointsList[currentWayPoint].GetEnemyCount() == 0)
        {
            if (currentWayPoint == wayPointsList.Count - 1)
            {
                sceneLogic.RestartGame();
                return;
            }
            currentWayPoint++;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = (hit.point - bulletSpawnPoint.position).normalized;

            GameObject bullet = bulletPooler.SpawnFromPool(bulletTag, bulletSpawnPoint.position, Quaternion.LookRotation(dir));
            bullet.GetComponent<Bullet>().AddInitialForce(dir);
        }
    }
}
