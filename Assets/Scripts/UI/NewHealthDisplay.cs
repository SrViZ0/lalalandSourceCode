using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewHealthDisplay : MonoBehaviour
{
    [Header("Health UI Variables")]
    public int maxHealth;
    public int health;
    public Sprite crackedHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public NewHealthSystem newhealthSystem;

    void Start()
    {
        
    }

    void Update()
    {
        health = newhealthSystem.playerHealth;
        maxHealth = newhealthSystem.maxHealth;

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
