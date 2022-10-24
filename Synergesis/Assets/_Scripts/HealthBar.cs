using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text maxHealth;
    public TMP_Text currentHealth;

    public void SetHealth(int health)
    {
        slider.value = health;
        currentHealth.text = health.ToString();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        maxHealth.text = health.ToString();
    }
}
