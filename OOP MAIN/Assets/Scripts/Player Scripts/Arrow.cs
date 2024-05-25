using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public float damage;
    Rigidbody2D rb;
    public Vector2 direction;
    void Start()
    {
        Vector2 vector2 = new Vector2(10f,10f);
        direction = PlayerController.Instance.shootDirection;
        //destroyTime();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*vector2;
    }

    void Update()
    {
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStats>().takeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
            Destroy(gameObject);
    }*/

    /*
    IEnumerator destroyTime()
    {
        Debug.Log("selam");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    */
}
