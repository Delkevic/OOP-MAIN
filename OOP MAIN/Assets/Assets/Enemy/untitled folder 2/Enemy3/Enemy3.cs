using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyHealth
{
    private float currentHealth;
    public float timer=1f;
    public bool isStunned;
    public Transform player;
    public GameObject triggerZone;
    private Animator an;
    private Rigidbody2D rb2;
    private Enemy3Move enemy3;
    private TriggerZone trigger;
    private ExplosionDamage explode;

    void Start()
    {
        currentHealth = maxHealth;
        an = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        explode = GetComponentInParent<ExplosionDamage>();
        enemy3 = GetComponentInParent<Enemy3Move>();
    }
    void Update()
    {
    }
    public override void TakeDamage(float damage)
    {
        if(!isStunned)
        {
            if(currentHealth>0)
            {
                FirstControl();
                currentHealth -= damage;
                an.SetBool("Stunned",true);
                StartCoroutine(StunnedCoroutine(timer));
            }
            else
            {   
                FirstControl();
                an.SetBool("Dead",true);
                currentHealth = 0;
                StartCoroutine(DeadCoroutine());
            
            }
        }
    }
    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        explode.Explode();
        Die();
    }
    IEnumerator StunnedCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        isStunned = false;
        an.SetBool("Stunned",false);
        an.SetBool("Move",true);
        enemy3.speed = 20f;
        if(trigger.isTrigger){
            enemy3.speed= 40f;
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void FirstControl()
    {
        isStunned = true;
        an.SetBool("Move",false);
        an.SetBool("MoveFast",false);
        an.SetBool("Idle",false);
        enemy3.speed =0f;
    }
}

