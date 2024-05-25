using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_Stats : EnemyHealth
{
    public bool isStunned ;
    public float stunnedTime = 3;
    private float currentHealth;
    public Transform player;
    private Rigidbody2D rb2;
    private Animator an;
    private Enemy4_Move enemy;
    void Start()
    {
        currentHealth = maxHealth;
        rb2 = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        stunnedTime -= Time.deltaTime;
        if(stunnedTime < 0)
        {
            isStunned = false;
            an.SetBool("isStunned", false);
        }
    }
        
    public override void TakeDamage(float damage)
    {
        isStunned = true;
        if(stunnedTime <=0)
        {
            currentHealth -= damage;
            an.SetBool("isStunned",true);
            stunnedTime = 3f;
        }
        if(currentHealth < 0)
        {
            currentHealth =0 ;
            Die();
        }
        
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

