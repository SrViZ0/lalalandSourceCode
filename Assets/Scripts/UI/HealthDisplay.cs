using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [Header("Health UI Variables")]
    public int maxHealth;
    public int health;
    public Sprite crackedHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public HealthSystem healthSystem;

    void Start()
    {
        
    }

    void Update()
    {
        health = healthSystem.playerHealth;
        maxHealth = healthSystem.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = crackedHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
