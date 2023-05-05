using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public TMPro.TextMeshProUGUI healthText;

    public Slider shieldSlider;
    public TMPro.TextMeshProUGUI shieldText;

    public Slider manaSlider;
    public TMPro.TextMeshProUGUI manaText;

    public TMPro.TextMeshProUGUI coinText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public GameObject DeathScreen;
    public GameObject PauseMenu, mapDisplay;

    public string newGameScene, menuScene;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, 0.25f * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }
    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
