using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyHealth : MonoBehaviour
{
    protected float maxHealth = 100f;
    protected float damage = 20f;
    private float timer;
    private  float knockBackForceX = 100f, knockBackForceY = 20f;
    private  float currentHealth;
    public Transform player;
    private Rigidbody2D rb2;
    private HitEffect hitEffect;
    void Start()
    {
        currentHealth = maxHealth;
        hitEffect = GetComponent<HitEffect>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Stunned();
        if(currentHealth < 0)
        {
            currentHealth =0 ;
            Destroy(gameObject);
        }
    }
    IEnumerator BackTheOriginal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = hitEffect.original;
    }
    private void Stunned()
    {
        if(player.position.x < transform.position.x)
        {
            rb2.AddForce(new Vector2(knockBackForceX,knockBackForceY),ForceMode2D.Force);
        }

        else
        {
            rb2.AddForce(new Vector2(-knockBackForceX,knockBackForceY),ForceMode2D.Force);
        }

        GetComponent<SpriteRenderer>().material = hitEffect.white;
        StartCoroutine(BackTheOriginal());
    }
}
