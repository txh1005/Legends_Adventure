using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int coinValue = 1;
    public float waitToPick = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToPick>0)
        {
            waitToPick -= Time.deltaTime;
        }   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="player"&&waitToPick<=0)
        {
            LevelManager.instance.GetCoins(coinValue);
            Destroy(gameObject);
            AudioManager.instance.PlayFSX(3);
        }
    }
}
