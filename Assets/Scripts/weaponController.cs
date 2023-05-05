using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public Animator anim1;
    public float delay = 0.3f;
    private bool attackBlock;
    /*public Transform circle;
    public float radius;*/
    public void attack()
    {
        if (attackBlock)
        {
            return;
        }
        anim1.SetTrigger("isAttack");
        attackBlock = true;
        StartCoroutine(DelayAttack());
    }

    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlock = false;
    }
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circle == null ? Vector3.zero : circle.position;
        Gizmos.DrawWireSphere(position, radius);
    }
    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circle.position,radius))
        {
            Debug.Log(collider.name);
        }
    }*/
}
