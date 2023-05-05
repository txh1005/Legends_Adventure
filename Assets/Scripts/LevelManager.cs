using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 2.5f;
    public string nextLevel;
    public bool isPause;
    public int currentCoints;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCoints = CharacterTracker.instance.currentCoins;
        UIController.instance.coinText.text = currentCoints.ToString();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public IEnumerator LevelEnd()
    {
        Player.instance.canMove = false;
        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextLevel);

        CharacterTracker.instance.currentCoins = currentCoints;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.currentMana = PlayerHealthController.instance.currentMana;
        CharacterTracker.instance.maxMana = PlayerHealthController.instance.maxMana;
        CharacterTracker.instance.currentShield = PlayerHealthController.instance.maxShield;
        CharacterTracker.instance.maxShield = PlayerHealthController.instance.maxShield;
    }
    public void PauseUnpause()
    {
        if (!isPause)
        {
            UIController.instance.PauseMenu.SetActive(true);
            isPause = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.PauseMenu.SetActive(false);
            isPause = false;
            Time.timeScale = 1f;
        }
    }
    public void GetCoins(int amount)
    {
        currentCoints += amount;
        UIController.instance.coinText.text = currentCoints.ToString();
    }
    public void SpendCoins(int amount)
    {
        currentCoints -= amount;
        if (currentCoints < 0)
        {
            currentCoints = 0;
        }
        UIController.instance.coinText.text = currentCoints.ToString();
    }
}
