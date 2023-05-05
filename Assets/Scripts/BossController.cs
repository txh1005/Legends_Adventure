using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    public Vector2 moveDirection;
    public Rigidbody2D rigid;

    public int currentHealth;
    public GameObject levelExit;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter>0)
        {
            actionCounter -= Time.deltaTime;
            //movement
            moveDirection = Vector2.zero;
            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChasePlayer)
                {
                    moveDirection = Player.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }
                if (actions[currentAction].moveToPoint)
                {
                    moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
                }
            }
            rigid.velocity = moveDirection * actions[currentAction].moveSpeed;
            //shot
            if (actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter<=0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;
                    foreach (Transform t in actions[currentAction].shotPoint)
                    {
                        Instantiate(actions[currentAction].itemToShoot,t.position,t.rotation);
                    }
                }
            }
        }
        else
        {
            currentAction++;
            if (currentAction>=actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;
        }
    }
    public void TakeDamage(int dameAmount)
    {
        currentHealth -= dameAmount;
        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
            levelExit.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="slash")
        {
            TakeDamage(5); 
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            PlayerHealthController.instance.DamagePlayer(1);
        }
    }
}
[System.Serializable]
public class BossAction
{
    public float actionLength;
    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;

    public bool moveToPoint;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoint;
}