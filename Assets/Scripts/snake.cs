using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float moveSpeed;
    public float rangeToChase;
    private Vector3 moveDirection;
    private Animator amin;
    public int health=10;
    public bool isBig;
    // Start is called before the first frame update
    void Start()
    {
        amin = GetComponent<Animator>();
        if (isBig)
        {
            transform.localScale = new Vector3(2f,2f,2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,Player.instance.transform.position)<rangeToChase)
        {
            if (Player.instance.transform.position.x<transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            moveDirection = Player.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        if (moveDirection!=Vector3.zero)
        {
            amin.SetBool("isMoving", true);
        }
        else
        {
            amin.SetBool("isMoving", false);
        }
        moveDirection.Normalize();
        rigid.velocity = moveDirection * moveSpeed;
    }
    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health<=0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlayFSX(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="slash")
        {
            DamageEnemy(5);
            AudioManager.instance.PlayFSX(2);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            PlayerHealthController.instance.DamagePlayer(2);
        }
    }
}
