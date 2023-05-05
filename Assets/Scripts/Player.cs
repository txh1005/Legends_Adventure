using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player instance;
    public float moveSpeed;
    public Vector2 moveInput;
    public Rigidbody2D rigid;
    public Transform weapon;
    private Animator anim;
    public Animator anim1;
    public weaponController weaponScript;
    public float delayAttack = 0.3f;


    private float acitveMoveSpeed;
    public float dashSpeed = 8f, dashLength = 0.5f, dashCooldown = 1f,dashInvin=0.5f;
    private float dashCounter, dashCoolCounter;

    public SpriteRenderer[] body;

    public bool canMove = true;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        acitveMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove&&!LevelManager.instance.isPause)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            rigid.velocity = moveInput * acitveMoveSpeed;
            //transform.position += new Vector3(moveInput.x, moveInput.y, 0f)*Time.deltaTime*moveSpeed;
            //rigid.velocity = moveInput * moveSpeed;
            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            if (mousePos.x > screenPoint.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                weapon.localScale = new Vector3(-1, 1, 1);
                weapon.rotation = Quaternion.Euler(0, 0, angle - 110);
            }
            else
            {
                transform.localScale = Vector3.one;
                weapon.localScale = Vector3.one;
                weapon.rotation = Quaternion.Euler(0, 0, angle - 150);
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlayerHealthController.instance.MinusMana();
                if (PlayerHealthController.instance.currentMana>0)
                {
                    weaponScript.attack();
                    AudioManager.instance.PlayFSX(7);
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    acitveMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    anim.SetTrigger("Dash");
                    AudioManager.instance.PlayFSX(8);
                }
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    acitveMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }
            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }
}
