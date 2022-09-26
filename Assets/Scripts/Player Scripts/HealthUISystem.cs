using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUISystem : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    public TextMeshProUGUI playerHealthText;
    private NameData nameData;

    private void Awake()
    {
        nameData = GameObject.Find("Name Data").GetComponent<NameData>();
    }

    void Start()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    private void Update()
    {
        SetHealth(playerHealth.currentHealth);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        playerHealthText.SetText( nameData.playerName + "'s HP: " + hp + "/" + playerHealth.maxHealth);
    }
}