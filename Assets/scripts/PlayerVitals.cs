using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerVitals : MonoBehaviour
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

    public Slider staminaSlider;
    public int maxStamina;
    private int staminaFallRate;
    public int staminaFallMult;
    private int staminaRegainRate;
    public int staminaRegainMult;

    public float deathHeight;

    private CharacterController charController;
    private FirstPersonController playerController;

    private Vector3 spawnPos;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        staminaFallRate = 1;
        staminaRegainRate = 1;

        charController = GetComponent<CharacterController>();
        playerController = GetComponent<FirstPersonController>();

        spawnPos = charController.transform.position;
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
            return;
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

        // Stamina control
        if (charController.velocity.magnitude > 0 && !playerController.m_IsWalking)
        {
            staminaSlider.value -= Time.deltaTime / staminaFallRate * staminaFallMult;
        }
        else
        {
            staminaSlider.value += Time.deltaTime / staminaRegainRate * staminaRegainMult;
        }

        if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            playerController.m_RunSpeed = playerController.m_WalkSpeed;
        }
        else
        {
            playerController.m_RunSpeed = playerController.m_RunSpeedNorm;
            if (staminaSlider.value >= maxStamina)
            {
                staminaSlider.value = maxStamina;
            }
        }

        if (charController.transform.position.y < deathHeight)
        {
            CharacterDeath();
            return;
        }
    }

    void CharacterDeath()
    {
        charController.transform.position = spawnPos;
        hungerSlider.value = maxHunger;
        thirstSlider.value = maxThirst;
        healthSlider.value = maxHealth;
        staminaSlider.value = maxStamina;
    }
}
