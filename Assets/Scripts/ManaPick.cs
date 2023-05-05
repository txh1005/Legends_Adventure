using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPick : MonoBehaviour
{
    public int manaPick = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            PlayerHealthController.instance.ManaPlayer(manaPick);
            Destroy(gameObject);
            AudioManager.instance.PlayFSX(4);
        }
    }
}
