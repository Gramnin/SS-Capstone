using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public bool singlePlayer;

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

    public GameObject deathPanel;

    private CharacterController charController;
    private FirstPersonController playerController;
    private MouseLook mouseLook;
    private float[] mouseSensitivity;

    private Vector3 spawnPos;

    public bool dead; // do not alter this variable from outside of this script

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        thirstSlider.maxValue = maxThirst;
        hungerSlider.maxValue = maxHunger;
        staminaSlider.maxValue = maxStamina;
        staminaFallRate = 1;
        staminaRegainRate = 1;

        charController = GetComponent<CharacterController>();
        playerController = GetComponent<FirstPersonController>();
        mouseLook = playerController.m_MouseLook;
        mouseSensitivity = new float[2] {mouseLook.XSensitivity, mouseLook.YSensitivity};

        spawnPos = charController.transform.position;

        CharacterRespawn();
    }

    void Update()
    {
        if (!dead)
        {
            /*
            if (Input.GetKey(KeyCode.Escape))
            {
                CharacterDeath();
                return;
            }
            */

            // Health control
            if (hungerSlider.value <= 0 && thirstSlider.value <= 0)
            {
                healthSlider.value -= Time.deltaTime / healthFallRate * 2;
            }
            else if (hungerSlider.value <= 0 || thirstSlider.value <= 0)
            {
                healthSlider.value -= Time.deltaTime / healthFallRate;
            }

            if (healthSlider.value > maxHealth)
            {
                healthSlider.value = maxHealth;
            }
            else if (healthSlider.value <= 0)
            {
                healthSlider.value = 0;
                CharacterDeath();
                return;
            }

            // Thirst control
            if (thirstSlider.value > 0)
            {
                thirstSlider.value -= Time.deltaTime / thirstFallRate;
            }

            if (thirstSlider.value <= 0)
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

            if (hungerSlider.value <= 0)
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
    }

    public void LockCursor(bool locking)
    {
        mouseLook.SetCursorLock(locking);
        if (locking)
        {
            mouseLook.XSensitivity = mouseSensitivity[0];
            mouseLook.YSensitivity = mouseSensitivity[1];
        }
        else
        {
            mouseLook.XSensitivity = 0;
            mouseLook.YSensitivity = 0;
        }
    }

    public void FreezeCharacter(bool freeze)
    {
        if (freeze)
        {
            if (singlePlayer)
            {
                Time.timeScale = 0;
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
                charController.enabled = false;
                playerController.m_UseHeadBob = false;
            }
        }
        else
        {
            Time.timeScale = 1;
            GetComponent<AudioSource>().mute = false;
            charController.enabled = true;
            playerController.m_UseHeadBob = true;
        }
        LockCursor(!freeze);
    }

    public void CharacterDeath()
    {
        dead = true;

        FreezeCharacter(true);

        healthSlider.value = 0;
        
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
        else
        {
            CharacterRespawn();
        }
    }

    public void CharacterRespawn()
    {
        charController.transform.position = spawnPos;
        hungerSlider.value = maxHunger;
        thirstSlider.value = maxThirst;
        healthSlider.value = maxHealth;
        staminaSlider.value = maxStamina;
        
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        FreezeCharacter(false);

        dead = false;
    }
}
