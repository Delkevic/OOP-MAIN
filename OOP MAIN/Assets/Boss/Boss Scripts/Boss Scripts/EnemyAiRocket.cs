using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiRocket : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bileþeni
    public GameObject rocketPrefab;    // Roket prefab'ý
    public float chaseDistance = 10f;  // Takip mesafesi
    public float rocketSpeed = 5f;     // Roket hýzý
    public float rocketInterval = 2f;  // Roket fýrlatma aralýðý
    public float rocketOffset = 1.5f;  // Roket doðma mesafesi (biraz daha uzak)

    private float nextRocketTime = 0f;

    public static EnemyAiRocket instance;

    void Update()
    {
        // Ana karakter ile düþman arasýndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Roket fýrlatma zamaný geldiyse
            if (Time.time >= nextRocketTime)
            {
                FireRocket();
                nextRocketTime = Time.time + rocketInterval;
            }
        }
    }

    void FireRocket()
    {
        if (rocketPrefab == null)
        {
            Debug.LogError("Rocket Prefab is not assigned!");
            return;
        }

        // Roketin yönünü belirle
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 rocketPosition = (Vector2)transform.position + direction * rocketOffset; // Bir buçuk birim offset

        // Roketi oluþtur
        GameObject rocket = Instantiate(rocketPrefab, rocketPosition, Quaternion.identity);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Roketin hýzýný belirle
            rb.velocity = direction * rocketSpeed;
        }

        // Rokete sahibinin tag'ýný bildir
        Rocket rocketScript = rocket.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.ownerTag = gameObject.tag;
        }
    }
}
