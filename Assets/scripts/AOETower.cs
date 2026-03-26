using System.Collections.Generic;
using UnityEngine;

public class AOETower : MonoBehaviour
{
   public float detectionRange = 5f;
    public float attackCooldown = 1f;
    public int damage = 10;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            AttackAllEnemies();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void AttackAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRange)
            {
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.DrawLine(transform.position, enemy.transform.position, Color.red, 0.2f);
                }
            }
        }
    }
}
