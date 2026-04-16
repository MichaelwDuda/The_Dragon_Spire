using UnityEngine;

public class HomeTowerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Vector3 originalScale;
    void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log("Home HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateTowerHeight()
    {
        float healthPercent = (float)currentHealth / maxHealth;

        // Only scale the height (Y axis)
        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * healthPercent,
            originalScale.z
        );
    }

    void Die()
    {
        Debug.Log("Game Over!");
        // You can add UI, restart, etc.
        Time.timeScale = 0f; // pauses the game
    }
}
