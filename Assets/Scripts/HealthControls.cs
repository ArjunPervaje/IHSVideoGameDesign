using UnityEngine;

public class HealthControls : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool healthDepleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (this.currentHealth <= 0)
        {
            healthDepleted = true;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents health from going below zero or above max
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public bool getIsHealthDepleted()
    {
        return this.healthDepleted;
    }
    

    //void UpdateHealthBar()
    //{
    //    healthBarFill.fillAmount = currentHealth / maxHealth;
    //}
}
