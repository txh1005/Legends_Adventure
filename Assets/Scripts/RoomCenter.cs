using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool open;
    public List<GameObject> enemies = new List<GameObject>();
    public Door theDoor;
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        if (open)
        {
            theDoor.close = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 /*&& theDoor.roomActive&& open*/)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0)
            {
                theDoor.OpenDoors();
                box.SetActive(true);
            }
        }
    }
}
