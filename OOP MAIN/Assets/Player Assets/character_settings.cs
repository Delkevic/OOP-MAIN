using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float hareketHizi;
    public float ziplamaHizi;
    private Animator animator;
    private Rigidbody2D rb;

   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    private void Update()
    {
       

        float yatayHareket = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(yatayHareket * hareketHizi, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaHizi);
        }

        if (yatayHareket != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        Vector3 yeniScale = transform.localScale;

        if (yatayHareket > 0)
        {
            yeniScale.x = Mathf.Abs(yeniScale.x); // Sað tarafa doðru hareket ederken
        }
        else if (yatayHareket < 0)
        {
            yeniScale.x = -Mathf.Abs(yeniScale.x); // Sol tarafa doðru hareket ederken
        }

        transform.localScale = yeniScale;

        
    }
    
}
