using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int rewardValue = 1;
    public float destroyDelayAfterDeath = 1.5f;
    public UnityEvent onDied;

    private int currentHealth;
    private bool isDead;
    private Enemy_move movement;
    private EnemyAnimationController animationController;
    private Collider enemyCollider;

    public int CurrentHealth => currentHealth;
    public bool IsDead => isDead;

    void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<Enemy_move>();
        animationController = GetComponent<EnemyAnimationController>();
        enemyCollider = GetComponent<Collider>();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        animationController?.PlayHit();
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        currentHealth = 0;

        if (movement != null)
        {
            movement.enabled = false;
        }

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        bool playedDeathAnimation = animationController != null && animationController.PlayDeath();

        onDied?.Invoke();
        Destroy(gameObject, playedDeathAnimation ? Mathf.Max(0f, destroyDelayAfterDeath) : 0f);
    }
}
