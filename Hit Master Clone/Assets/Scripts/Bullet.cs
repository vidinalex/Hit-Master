using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float damage, speed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy) enemy.RecieveDamage(damage);
        }
        gameObject.SetActive(false);
    }

    public void AddInitialForce(Vector3 dir)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = dir * speed;
    }
}
