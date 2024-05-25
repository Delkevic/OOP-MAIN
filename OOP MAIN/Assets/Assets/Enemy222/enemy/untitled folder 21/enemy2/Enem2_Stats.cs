using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem2_Stats : EnemyHealth
{   private float currenHealth;
    private Animator an;
    public bool isStunned;
    private Enemy2 enemy2;
    private float timer=0.8f;

    void Start()
    {
        an = GetComponent<Animator>();
        enemy2=GetComponentInParent<Enemy2>();
        currenHealth = maxHealth;
    }

    public override void TakeDamage(float damage)
    {
        if(!isStunned)
        {
            if(currenHealth >0)
            {
                currenHealth -= damage;
                an.SetBool("CanWalk", false);
                an.SetBool("Attack", false);
                an.SetBool("Hit",true);
                enemy2.moveSpeed = 0f;
                StartCoroutine(StunnedCoroutine(timer));
            }
            else
            {
                currenHealth =0;
                an.SetBool("CanWalk", false);
                an.SetBool("Attack", false);
                an.SetBool("Death",true);
                enemy2.moveSpeed=0f; 
                StartCoroutine(DeadCoroutine(0.5f));
            }


        }

    }
    IEnumerator StunnedCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        isStunned = false;
        an.SetBool("Hit",false);
        enemy2.moveSpeed = 2f;
        an.SetBool("CanWalk",true);
    }
    IEnumerator DeadCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
