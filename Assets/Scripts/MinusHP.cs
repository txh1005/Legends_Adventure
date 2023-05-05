using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusHP : MonoBehaviour
{
    public static MinusHP instance;
    public TMPro.TextMeshProUGUI minus;
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
