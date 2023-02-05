using System;
using UnityEngine;
public class HealthSystem
{

    public HealthSystem(int maxHealth, int health)
    {
        if (health > maxHealth)
        {
            this.health = maxHealth;
        }
        else
        {
            this.health = health;
        }
        this.maxHealth = maxHealth;
    }

    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;
    private int maxHealth;
    private int health;
    public int GetHealth()
    {
        return health;
    }

    public float GetPercentHealth()
    {
        return (float)health / maxHealth;
    }

    public int GetMaxHelath()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Heal(int healAmount)
    {
        Debug.Log("player healed");
        int tempHp = health + healAmount;
        if (tempHp > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = tempHp;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int damageAmount)
    {
        if (health <= 0) return;
        health -= damageAmount;
        Debug.Log("player took damage");
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if (health <= 0)
        {
            health = 0;
            OnDeath.Invoke(this, EventArgs.Empty);
        }
        
    }

}
