using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D projectileRb;
    public float speed;

    public float projectileLife;
    public float projectileCount;
    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount-= Time.deltaTime;
        if(projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        projectileRb.velocity= new Vector2(speed,projectileRb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
