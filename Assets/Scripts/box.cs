using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    private Animator anim;
    public bool shouldDropItem;
    public GameObject[] itemToDrop;
    public float itemDropPercentHP, itemDropPercentCoin;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void open()
    {
        anim.SetBool("openBox",true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="player"|| other.tag == "slash")
        {
            open();
            if (shouldDropItem)
            {
                int numberOfItems = Random.Range(1, 3);
                int numberOfItemsCoin = Random.Range(6, 10);
                float dropChane = Random.Range(0, 100);
                if (dropChane < itemDropPercentHP)
                {
                    for (int i = 0; i < numberOfItems; i++)
                    {
                        //int randomItem = Random.Range(0,itemToDrop.Length);
                        float randomPosX = Random.Range(-0.5f, 0.5f);
                        float randomPosY = Random.Range(-0.5f, 0.5f);
                        Instantiate(itemToDrop[0], transform.position + new Vector3(randomPosX, randomPosY), transform.rotation);
                    }
                    
                }
                if (dropChane<itemDropPercentCoin)
                {
                    for (int i = 0; i < numberOfItemsCoin; i++)
                    {
                        //int randomItem = Random.Range(0,itemToDrop.Length);
                        float randomPosX = Random.Range(-0.5f, 0.5f);
                        float randomPosY = Random.Range(-0.5f, 0.5f);
                        Instantiate(itemToDrop[1], transform.position + new Vector3(randomPosX, randomPosY), transform.rotation);
                    }
                }
            }
        }     
    }
}
