using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource levelMusic, gameOverMusic, WinMusic;
    public AudioSource[] sfx;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }
    public void PlayWin()
    {
        levelMusic.Stop();
        WinMusic.Play();
    }
    public void PlayFSX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }    
}
