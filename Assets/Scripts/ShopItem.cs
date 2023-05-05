using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject buyText;
    private bool inBuyZone;
    public bool isHealRestore, isFullHealRestore, isManaRestore, isManaUpgrade,isHealUpgrade,isShieldUpgrade;
    public int itemCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.currentCoints>=itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);
                    if (isHealRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(1);
                    }
                    if (isFullHealRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }
                    if (isManaRestore)
                    {
                        PlayerHealthController.instance.ManaPlayer(20);
                    }
                    if (isManaUpgrade)
                    {
                        PlayerHealthController.instance.UpgradeMaxMana(20);
                    }
                    if (isHealUpgrade)
                    {
                        PlayerHealthController.instance.UpgradeMaxHeal(2);
                    }
                    if (isShieldUpgrade)
                    {
                        PlayerHealthController.instance.UpgradeMaxShield(2);
                    }
                    AudioManager.instance.PlayFSX(9);
                    gameObject.SetActive(false);
                    inBuyZone = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="player")
        {
            buyText.SetActive(true);
            inBuyZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            buyText.SetActive(false);
            inBuyZone = false;
        }
    }
}
