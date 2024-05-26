using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float explosionRadius = 2f;   // Patlama yarýçapý
    public float explosionForce = 700f;  // Patlama kuvveti
    public int damage = 50;              // Hasar miktarý
    public string ownerTag;              // Roketin sahibi
    

    // Patlama efekti
    public GameObject explosionEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")|| collision.CompareTag("Ground"))
        {
            // Patlama etkisi yarat
            Explode();
            // Roketi yok et
            Destroy(gameObject); 
        }
    }

    void Explode()
    {
        // Patlama efekti oluþtur
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Patlama alanýndaki tüm nesneleri bul
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            if (nearbyObject.CompareTag("Player"))
            {
                // Ana karaktere hasar ver
                PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }
}

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var explosionDir = rb.position - (Vector2)explosionPosition;
        float explosionDistance = explosionDir.magnitude;
        if (explosionDistance <= explosionRadius)
        {
            float force = Mathf.Lerp(0, explosionForce, (explosionRadius - explosionDistance) / explosionRadius);
            rb.AddForce(explosionDir.normalized * force);
        }
    }
}
