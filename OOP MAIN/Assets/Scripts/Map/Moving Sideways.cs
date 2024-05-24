using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingSideways : MonoBehaviour
{
    bool moveToA = true;

    public float speed;

    public GameObject pointA, pointB;

    Rigidbody2D rb;

    void Update()
    {
        if (moveToA)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Debug.Log("gidiyor");
            if (Vector2.Distance(transform.position, pointA.transform.position) < 0.2f)
                moveToA = false;
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
            if (Vector2.Distance(transform.position, pointB.transform.position) < 0.2f)
                moveToA = true;
        }
    }
}
