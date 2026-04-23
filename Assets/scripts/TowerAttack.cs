using UnityEngine;

public class TowerAttack : TowerBase
{
    public float detectionRange = 5f;
    public float attackCooldown = 1f;
    public int damage = 10;
    public int maxTargets = 3;

    public GameObject projectile;
    private float nextAttackTime = 0f;
    private Transform currentTarget;

    void Update()
    {
        FindTarget();

        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);

            if (distance <= detectionRange && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance && distance <= detectionRange)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        currentTarget = closestEnemy;
    }

    void Attack()
    {
        if (currentTarget == null) return;

        EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        if (projectile != null)
        {
            Instantiate(
                projectile,
                currentTarget.position,
                Quaternion.identity
            );
        }
    }
}
