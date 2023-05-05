using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private bool canSelect;
    public GameObject message;
    public Player playerToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = Player.instance.transform.position;
                Destroy(Player.instance.gameObject);
                Player newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                Player.instance = newPlayer;

                gameObject.SetActive(false);

                CameraMovement.instance.target = newPlayer.transform.gameObject;
            }
        }   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }
}
