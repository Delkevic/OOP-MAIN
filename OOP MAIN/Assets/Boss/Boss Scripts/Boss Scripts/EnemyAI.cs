using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bile�eni
    public GameObject fireballPrefab;  // Ate� topu prefab'�
    public float chaseDistance = 10f;  // Takip mesafesi
    public float fireballSpeed = 5f;   // Ate� topu h�z�
    public float fireballInterval = 2f; // Ate� topu f�rlatma aral���
    public float fireballOffset = 1.0f; // Ate� topu do�ma mesafesi

    private float nextFireballTime = 0f;

    void Update()
    {
        // Ana karakter ile d��man aras�ndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Ate� topu f�rlatma zaman� geldiyse
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

        // Ate� topunun y�n�n� belirle
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 fireballPosition = (Vector2)transform.position + direction * fireballOffset; // Bir birim offset

        // Ate� topunu olu�tur
        GameObject fireball = Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Ate� topunun h�z�n� belirle
            rb.velocity = direction * fireballSpeed;
        }

        // Ate� topuna sahibinin tag'�n� bildir
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetOwnerTag(gameObject.tag);
        }
    }
}
