using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] RectTransform hpBar;

    private float maxHP;
    private SpawnPoint localSpawnPoint;
    private List<Collider> rigColliders = new List<Collider>();
    private List<Rigidbody> rigRigidbodies = new List<Rigidbody>();
    private Animator animator;
    private Collider localCollider;


    private void Start()
    {
        maxHP = hp;
        rigColliders.AddRange(GetComponentsInChildren<Collider>());
        rigRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());

        animator = GetComponent<Animator>();
        localCollider = GetComponent<Collider>();

        RigidbodyState(true);
        ColliderState(false);
    }

    public void SetLocalSpawnPoint(SpawnPoint localSpawnPoint)
    {
        this.localSpawnPoint = localSpawnPoint;
    }

    public void RecieveDamage(float damage)
    {
        if (hp < 0) return;

        hp -= damage;
        hpBar.sizeDelta = new Vector2(hp / maxHP * 0.5f, hpBar.sizeDelta.y);
        if (hp <= 0)
        {
            hpBar.sizeDelta = new Vector2(0, 0);
            Death();
        }
    }

    private void Death()
    {
        localSpawnPoint.LocalEnemyDead();
        localCollider.isTrigger = true;

        ColliderState(true);
        RigidbodyState(false);

        animator.enabled = false;
    }

    private void ColliderState(bool state)
    {
        foreach (Collider col in rigColliders)
        {
            col.enabled = state;
        }
        localCollider.enabled = !state;
    }
    private void RigidbodyState(bool state)
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = state;
        }
    }
}
