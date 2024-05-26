using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bileþeni
    public GameObject fireballPrefab;  // Ateþ topu prefab'ý
    public float chaseDistance = 10f;  // Takip mesafesi
    public float fireballSpeed = 5f;   // Ateþ topu hýzý
    public float fireballInterval = 2f; // Ateþ topu fýrlatma aralýðý
    public float fireballOffset = 1.0f; // Ateþ topu doðma mesafesi

    private float nextFireballTime = 0f;

    void Update()
    {
        // Ana karakter ile düþman arasýndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Ateþ topu fýrlatma zamaný geldiyse
            if (Time.time >= nextFireballTime)
            {
                FireFireball();
                nextFireballTime = Time.time + fireballInterval;
            }
        }
    }

    void FireFireball()
    {
        if (fireballPrefab == null)
        {
            Debug.LogError("Fireball Prefab is not assigned!");
            return;
        }

        // Ateþ topunun yönünü belirle
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 fireballPosition = (Vector2)transform.position + direction * fireballOffset; // Bir birim offset

        // Ateþ topunu oluþtur
        GameObject fireball = Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Ateþ topunun hýzýný belirle
            rb.velocity = direction * fireballSpeed;
        }

        // Ateþ topuna sahibinin tag'ýný bildir
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetOwnerTag(gameObject.tag);
        }
    }
}
