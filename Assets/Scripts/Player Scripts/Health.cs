using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 100;

    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    // ABSTRACTION
    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;

        healthBar.value = currentHealth;
    }

    // ABSTRACTION
    public void HealPlayer(int heal)
    {
        currentHealth += heal;

        healthBar.value = currentHealth;
    }
}