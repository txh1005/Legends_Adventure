using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;
    private Animator amin;
    // Start is called before the first frame update
    void Start()
    {
        amin = GetComponent <Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="player")
        {
            //SceneManager.LoadScene(levelToLoad);
            amin.SetBool("isTele", true);
            StartCoroutine(LevelManager.instance.LevelEnd());
            LevelManager.instance.currentCoints += 10;
            UIController.instance.coinText.text = LevelManager.instance.currentCoints.ToString();
            
        }
    }
}
