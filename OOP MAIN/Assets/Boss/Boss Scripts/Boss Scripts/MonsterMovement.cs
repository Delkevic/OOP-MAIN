using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] patrolPoints; // Devriye noktalarý
    public float moveSpeed; // Hareket hýzý
    public int patrolDestination; // Mevcut devriye hedefi
    public Transform playerTransform; // Oyuncunun Transform bileþeni
    public bool isChasing; // Takip edip etmediðini belirler
    public float chaseDistance; // Takip etme mesafesi

    // Update is called once per frame
    void Update()
    {
        // Oyuncu ile canavar arasýndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Oyuncuyu belirli bir mesafede görürse takibe baþla
        if (distanceToPlayer < chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            // Takip etme hareketi
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-0.2f, 0.2f, 1.0f);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Devriye hareketi
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {
                    patrolDestination = 1;
                }
            }
            else if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                {
                    patrolDestination = 0;
                }
            }

            // Yön deðiþtirme
            if (transform.position.x < patrolPoints[patrolDestination].position.x)
            {
                transform.localScale = new Vector3(-0.2f, 0.2f, 1.0f);
            }
            else if (transform.position.x > patrolPoints[patrolDestination].position.x)
            {
                transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}
