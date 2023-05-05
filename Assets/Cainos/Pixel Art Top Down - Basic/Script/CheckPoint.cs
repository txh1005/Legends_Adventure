using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CheckPoint : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text textCheckPoint;
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            /*else
            {
                dialogBox.SetActive(true);
                textCheckPoint.text = text;
            }*/
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            dialogBox.SetActive(true);
            textCheckPoint.text = text;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            dialogBox.SetActive(false);
        }
    }
}
