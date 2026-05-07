using UnityEngine;

public class FireBreathBehavior : MonoBehaviour
{
    public int damagePerTick = 5;
    public float tickRate = 0.2f;
    private float timer;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding: " + other.name);
        timer += Time.deltaTime;
        if (timer < tickRate) return;
        timer = 0f;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damagePerTick);
        }
    }
}
