using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiRocket : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bile�eni
    public GameObject rocketPrefab;    // Roket prefab'�
    public float chaseDistance = 10f;  // Takip mesafesi
    public float rocketSpeed = 5f;     // Roket h�z�
    public float rocketInterval = 2f;  // Roket f�rlatma aral���
    public float rocketOffset = 1.5f;  // Roket do�ma mesafesi (biraz daha uzak)

    private float nextRocketTime = 0f;

    public static EnemyAiRocket instance;

    void Update()
    {
        // Ana karakter ile d��man aras�ndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Roket f�rlatma zaman� geldiyse
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

        // Roketin y�n�n� belirle
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 rocketPosition = (Vector2)transform.position + direction * rocketOffset; // Bir bu�uk birim offset

        // Roketi olu�tur
        GameObject rocket = Instantiate(rocketPrefab, rocketPosition, Quaternion.identity);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Roketin h�z�n� belirle
            rb.velocity = direction * rocketSpeed;
        }

        // Rokete sahibinin tag'�n� bildir
        Rocket rocketScript = rocket.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.ownerTag = gameObject.tag;
        }
    }
}
