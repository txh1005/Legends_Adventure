using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,attack,interact
}
public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;
    public float speed=30f;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private Vector3 respawnPoint;
    Vector2 movementInput;
    CheckPoint a;
    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        //set x=0, y=1 nhan vat huong len va khi tan cong dung yen se hitbox se khong bi bat tat ca len
        animator.SetFloat("X",0);
        animator.SetFloat("Y",1);
    }
    private void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");//change GetAxis(-1 to 1)-GetAxisRaw(-1|0|1) run hitbox
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack")&&currentState!=PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState==PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }     
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("isAttack", true);
        currentState = PlayerState.attack;
        yield return null;//1frame
        animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }    
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("X", change.x);
            animator.SetFloat("Y", change.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hole"))
        {
            transform.position = respawnPoint;
            a.dialogBox.SetActive(false);
        }
        else if (other.CompareTag("checkPoint"))
        {
            respawnPoint = transform.position;
        }
    }
}
