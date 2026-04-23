using System.Collections.Generic;
using UnityEngine;

public class AOETower : TowerBase
{
   public float detectionRange = 5f;
    public float attackCooldown = 1f;
    public int damage = 10;

    public GameObject AoeA;
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
        if (AoeA != null)
            Instantiate(AoeA, transform.position, Quaternion.identity);
            
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
                }
            }
        }
    }
}
