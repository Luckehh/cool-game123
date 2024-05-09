using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()    
    {
        currentHealth = maxHealth;
    }

    // Function to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to heal the player
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    // Function to handle player death
    void Die()
    {
        // You can add death-related logic here, such as disabling player controls, triggering an animation, etc.
        Debug.Log("Player died!");
    }
}