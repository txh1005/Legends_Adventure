using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public static enemy instance;
    public float speed;
    public float stopDistance;
    public float rangeToChase=30;
    public int health=20;

    private float timeBtwShot;
    public float startTimeBtwShot;

    public Transform player;
    private Animator amin;
    public GameObject bullet;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        amin = GetComponent<Animator>();
        timeBtwShot = startTimeBtwShot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.gameObject.activeInHierarchy/*Door.instance.roomActive*/)
        {
            if (transform.position.x >= player.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Vector2.Distance(transform.position, player.position) < rangeToChase)
            {


                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {
                    amin.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, player.position) < stopDistance)
                {
                    amin.SetBool("isMoving", false);
                    transform.position = this.transform.position;
                }
                /*else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                }*/

                if (timeBtwShot <= 0 )
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    timeBtwShot = startTimeBtwShot;
                }
                else
                {
                    timeBtwShot -= Time.deltaTime;
                }
            }
            else
            {
                amin.SetBool("isMoving", false);
            }
        }

    }
    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlayFSX(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "slash")
        {
            DamageEnemy(5);
            AudioManager.instance.PlayFSX(2);
        }

    }
}
