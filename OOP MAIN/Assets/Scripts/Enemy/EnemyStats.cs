using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public GameObject deathEffect;
    public float timer;
    //HitEffect effect;
    Rigidbody2D rb;
    public float knockBackForceX, knockBackForceY;
    public Transform player;
    public float damage;
    public float expToGive;
    public AudioSource hitAS, deadAS;

    public GameObject[] lootItems;
    void Start()
    {
        currentHealth = maxHealth;
        //effect = GetComponent<HitEffect>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        AudioManager.instance.PlayAudio(hitAS);
        if (player.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }

        //GetComponent<SpriteRenderer>().material = effect.white;
        StartCoroutine(BackToNormal());
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Instantiate(deathEffect, transform.position, transform.rotation);

            Instantiate(lootItems[0],transform.position,Quaternion.identity);
            Destroy(gameObject);
            Experience.instance.expMod(expToGive);
            AudioManager.instance.PlayAudio(deadAS);
        }
    }

    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);
        //GetComponent<SpriteRenderer>().material = effect.original;
    }
}
