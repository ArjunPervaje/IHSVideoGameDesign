using System.Collections;
using UnityEngine;

public class HealthControls : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool healthDepleted;
    public bool isPlayer;
    private bool canTakeDamage;
    public float IVFrameTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
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
        if (canTakeDamage)
        {
            canTakeDamage = false;
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents health from going below zero or above max
            StartCoroutine(IVFrameTimer());
        }
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

    IEnumerator IVFrameTimer()
    {
        yield return new WaitForSeconds(IVFrameTime);

        canTakeDamage = true;
    }

    public float getHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    //void UpdateHealthBar()
    //{
    //    healthBarFill.fillAmount = currentHealth / maxHealth;
    //}
}
