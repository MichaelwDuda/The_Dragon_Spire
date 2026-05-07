using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage = 8;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit " + other.name);

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
