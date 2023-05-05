using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public int webDame=1;
    private Transform player;
    private Vector2 target;
    private Animator amin;
    void Start()
    {
        amin = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }
    void Update()
    {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                DestroyBullet();
            }  
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="player")
        {
            amin.SetBool("isHit", true);
            StartCoroutine(DestroyAfterDelay());
            PlayerHealthController.instance.DamagePlayer(webDame);
        }
        else
        {
            amin.SetBool("isHit", true);
            StartCoroutine(DestroyAfterDelay());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            amin.SetBool("isHit", true);
            StartCoroutine(DestroyAfterDelay());
            PlayerHealthController.instance.DamagePlayer(webDame);
        }
        else
        {
            amin.SetBool("isHit", false);
            Destroy(gameObject);
        } 
            
                
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        DestroyBullet();
    }
}
