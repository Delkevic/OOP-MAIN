using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    private float lifeTime = 3f;
    public float bulletSpeed=100f;
    public float bulletDamage=1f;
    public Rigidbody2D rb2;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        rb2 = GetComponent<Rigidbody2D>();
        float direction = transform.localScale.x;  
        rb2.velocity = new Vector2(direction * bulletSpeed, 0); 
        Destroy(gameObject, lifeTime); 
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(bullet);
        }
    }
    
}
