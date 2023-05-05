using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;

    public int currentShield;
    public int maxShield;

    public int currentMana;
    public int maxMana;

    public float dameInvinLength = 1f;
    private float invinCount;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;
        maxMana = CharacterTracker.instance.maxMana;
        currentMana = CharacterTracker.instance.currentMana;
        maxShield = CharacterTracker.instance.maxShield;
        currentShield = CharacterTracker.instance.currentShield;

        /*currentHealth = maxHealth;
        currentShield = maxShield;
        currentMana = maxMana;*/

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        UIController.instance.shieldSlider.maxValue = maxShield;
        UIController.instance.shieldSlider.value = currentShield;
        UIController.instance.shieldText.text = currentShield.ToString() + " / " + maxShield.ToString();

        UIController.instance.manaSlider.maxValue = maxMana;
        UIController.instance.manaSlider.value = currentMana;
        UIController.instance.manaText.text = currentMana.ToString() + " / " + maxMana.ToString();

        //MinusHP.instance.gameObject.SetActive(false);
        InvokeRepeating("IncreaseShield", 8f, 6f);

        //UIController.instance.DeathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (invinCount > 0)
        {
            invinCount -= Time.deltaTime;
            if (invinCount <= 0)
            {
                for (int i = 0; i < Player.instance.body.Length; i++)
                {
                    Player.instance.body[i].color = new Color(Player.instance.body[i].color.r, Player.instance.body[i].color.g, Player.instance.body[i].color.b, 1f);
                }
            }
        }
    }
    public void DamagePlayer(int dame)
    {
        if (invinCount <= 0)
        {
            currentShield -= dame;

            if (currentShield < 0)
            {
                invinCount = dameInvinLength;
                for (int i = 0; i < Player.instance.body.Length; i++)
                {
                    Player.instance.body[i].color = new Color(Player.instance.body[i].color.r, Player.instance.body[i].color.g, Player.instance.body[i].color.b, 0.5f);
                }
                currentHealth -= dame;
                currentShield = 0;
                AudioManager.instance.PlayFSX(5);
                /*MinusHP.instance.minus.text = dame.ToString();
                MinusHP.instance.gameObject.SetActive(true);
                StartCoroutine(HideMinusHP());*/
            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Player.instance.gameObject.SetActive(false);
                AudioManager.instance.PlayFSX(6);
                UIController.instance.DeathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

            UIController.instance.shieldSlider.value = currentShield;
            UIController.instance.shieldText.text = currentShield.ToString() + " / " + maxShield.ToString();
        }
    }
    IEnumerator HideMinusHP()
    {
        yield return new WaitForSeconds(0.5f);
        MinusHP.instance.gameObject.SetActive(false);
    }
    void IncreaseShield()
    {
        if (currentShield < maxShield)
        {
            currentShield++;
            UIController.instance.shieldSlider.value = currentShield;
            UIController.instance.shieldText.text = currentShield.ToString() + " / " + maxShield.ToString();
        }
    }
    public void MinusMana()
    {
        currentMana--;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
        UIController.instance.manaSlider.value = currentMana;
        UIController.instance.manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
    }
    public void HealPlayer(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void ManaPlayer(int mana)
    {
        currentMana += mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        UIController.instance.manaSlider.value = currentMana;
        UIController.instance.manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
    }
    public void UpgradeMaxMana(int amount)
    {
        maxMana += amount;
        currentMana = maxMana;

        UIController.instance.manaSlider.maxValue = maxMana;
        UIController.instance.manaSlider.value = currentMana;
        UIController.instance.manaText.text = currentMana.ToString() + " / " + maxMana.ToString();
    }
    public void UpgradeMaxHeal(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void UpgradeMaxShield(int amount)
    {
        maxShield += amount;
        currentShield = maxShield;

        UIController.instance.shieldSlider.maxValue = maxShield;
        UIController.instance.shieldSlider.value = currentShield;
        UIController.instance.shieldText.text = currentShield.ToString() + " / " + maxShield.ToString();
    }
}
