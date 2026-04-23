using System.Collections.Generic;
using UnityEngine;

public class AOETower : TowerBase
{
    public GameObject AoeA;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            AttackAllEnemies();
            nextAttackTime = Time.time + fireRate;
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

            if (distance <= range)
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
