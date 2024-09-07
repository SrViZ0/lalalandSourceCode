using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthChecker : MonoBehaviour
{
    public int maxHealth;
    public static int playerHealth;

    public Slider playerHealthSlider;
    public TextMeshProUGUI playerHealthText;
    void Start()
    {
        playerHealth = maxHealth;
        playerHealthSlider.maxValue = maxHealth;
    }
    private void Update()
    {
        playerHealthSlider.value = playerHealth;
        playerHealthText.text = "Health: " + playerHealth + " / " + maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage()
    {
        playerHealth -= 1;
    }
}
