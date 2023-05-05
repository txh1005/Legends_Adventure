using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rigid;
    public float speed;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigid.rotation = angel+90;
        dir.Normalize();
        movement = dir;
    }
    private void FixedUpdate()
    {
        //moveSpider(movement);
    }
    void moveSpider(Vector2 dire)
    {
        rigid.MovePosition((Vector2)transform.position+(dire* speed * Time.deltaTime));
    }
}
