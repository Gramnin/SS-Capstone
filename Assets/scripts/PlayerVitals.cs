using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerVitals : MonoBehavior
{
    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;

    public Slider thirstSlider;
    public int maxThirst;
    public int thirstFallRate;

    public Slider hungerSlider;
    public int maxHunger;
    public int hungerFallRate;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
    }

    void Update()
    {
        // Health control
        if (hungerSlider.value <= 0 && thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate * 2;
        }
        else if (hungerSlider.value <= 0 || thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate;
        }
        else if (healthSlider.value > maxHealth)
        {
            healthSlider.value = maxHealth;
        }
        else if (healthSlider.value <= 0)
        {
            CharacterDeath();
        }

        // Thirst control
        if (thirstSlider.value > 0)
        {
            thirstSlider.value -= Time.deltaTime / thirstFallRate;
        }
        else if (thirstSlider.value <= 0)
        {
            thirstSlider.value = 0;
        }
        else if (thirstSlider.value > maxThirst)
        {
            thirstSlider.value = maxThirst;
        }

        // Hunger control
        if (hungerSlider.value > 0)
        {
            hungerSlider.value -= Time.deltaTime / hungerFallRate;
        }
        else if (hungerSlider.value <= 0)
        {
            hungerSlider.value = 0;
        }
        else if (hungerSlider.value > maxHunger)
        {
            hungerSlider.value = maxHunger;
        }
    }

    void CharacterDeath()
    {
        ;
    }
}
