using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour
{
    public float yatayhareket;
    public float harekethýzý;
    public float zýplamahýzý;
    Animator animator;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        yatayhareket = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(yatayhareket * harekethýzý * 60 * Time.deltaTime, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(yatayhareket * harekethýzý * Time.deltaTime, zýplamahýzý * 250 * Time.deltaTime);
        }

        Vector2 yeniscale = transform.localScale;

        if (yatayhareket > 0)
        {
            yeniscale.x = 0.5f;
        }
        if (yatayhareket < 0)
        {
            yeniscale.x = -0.5f;
        }

        if (yatayhareket != 0)
        {
            animator.SetBool("run", true);

        }
        else
        {
            animator.SetBool("run", false);
        }
        transform.localScale = yeniscale;
    }
}
