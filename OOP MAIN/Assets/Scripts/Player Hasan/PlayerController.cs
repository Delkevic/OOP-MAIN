using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float climbSpeed;
    public float movementDirection;
    public float speed = 4f;
    public float jumpPower = 5f;
    public float radius;
    public float attackRate = 2f;
    float nextAttack = 0f;
    float nextShoot = 0f;
    float movementCheck;
    float dashTime = 0.3f;
    float num = 0f;
    public float attackDistance;
    public float damage = 25f;
    public float tempDamage;
    float nextChange = 0;
    float changeTime = 2f;

    private bool isFaceRight = true;
    bool isGrounded;
    bool isGrounded2;
    bool sjump = true;
    bool canClimbR = false;
    bool canClimbR2 = false;
    bool canDash = true;
    bool isDashing = false;
    bool isEmirHoca = true;
    public bool isBlocking = false;

    Rigidbody2D rb;

    public Transform attackPoint;

    public LayerMask enemyLayers;
    public LayerMask layer;

    public GameObject circle, circle2, circleR, circleR2, arrow, shootPointObj,cameraa;

    Animator anim;

    public Text currentXPText;

    public Vector2 shootDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentXPText.text = Experience.instance.currentExperience.ToString() + "/" + Experience.instance.expToNextLevel.ToString();
    }


    void Update()
    {
        if (GetComponent<PlayerHealth>().isDying == true)
            return;
        block();
        if (isDashing || isBlocking)
            return;
        wallClimb();
        CheckDirection();
        jump();
        surfaceCheck();
        checkAnimatoins();
        dashCheck();
        AttackKnt();
        characterChange();

        arrowCheck();
    }

    private void FixedUpdate()
    {
        if (PlayerHealth.instance.isDying == true)
            return;
        if (isDashing || isBlocking)
            return;
        Movement();
    }

    public void characterChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Time.time > nextChange)
            {
                isEmirHoca = !isEmirHoca;
                nextChange = Time.time + 1f / changeTime;
            }
        }
    }

    public void dashCheck()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && !canClimbR)
        {
            StartCoroutine(dash());
        }
    }

    public void Attack()
    {
        if (num != 3f && isEmirHoca)
        {
            anim.SetTrigger("atk1");
            num++;
            tempDamage = damage;
        }
        if (num == 3 && isEmirHoca)
        {
            anim.SetTrigger("atk2");
            num = 0f;
            tempDamage = damage * 1.5f;
        }

        if (!isEmirHoca)
        {
            anim.SetTrigger("atk1");
            tempDamage = damage - 10;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<EnemyStats>().takeDamage(tempDamage);
            currentXPText.text = Experience.instance.currentExperience.ToString() + "/" + Experience.instance.expToNextLevel.ToString();
        }
    }

    private void AttackKnt()
    {
        if (Time.time > nextAttack && isGrounded && isGrounded2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                if (isEmirHoca)
                    nextAttack = Time.time + 1f / attackRate;
                else
                    nextAttack = Time.time + 1f / (attackRate * 1.5f);
            }
        }
    }

    public void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        if (isEmirHoca)
        {
            anim.SetBool("isEmir", true);
            rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
            anim.SetFloat("run", Math.Abs(movementDirection * speed));

        }
        else
        {
            anim.SetBool("isEmir", false);
            rb.velocity = new Vector2(movementDirection * speed * 1.4f, rb.velocity.y);
            anim.SetFloat("run_oguz", Math.Abs(movementDirection * speed));
        }
    }

    void checkAnimatoins()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    public void CheckDirection()
    {
        if (movementDirection != 0)
        {
            if (isFaceRight && movementDirection < 0)
                Flip();
            else if (!isFaceRight && movementDirection > 0)
                Flip();
        }
    }

    public void Flip()
    {
        isFaceRight = !isFaceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //transform.Rotate(0f,180f,0f);
    }

    public void jump()
    {
        if (isGrounded && isGrounded2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            sjump = true;
            canDash = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && sjump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                sjump = false;
            }
        }
    }

    public void wallClimb()
    {
        if (canClimbR || canClimbR2)
        {
            if (Input.GetKey(KeyCode.W))
                rb.velocity = new Vector2(0, climbSpeed);
            else if (Input.GetKeyDown(KeyCode.Space))
                rb.velocity = new Vector2(2, 2);
            sjump = true;
            canDash = true;
        }
    }

    public void surfaceCheck()
    {
        isGrounded = Physics2D.OverlapCircle(circle.transform.position, radius, layer);
        isGrounded2 = Physics2D.OverlapCircle(circle2.transform.position, radius, layer);
        canClimbR = Physics2D.OverlapCircle(circleR.transform.position, radius, layer);
        canClimbR2 = Physics2D.OverlapCircle(circleR2.transform.position, radius, layer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(circle.transform.position, radius);
        Gizmos.DrawWireSphere(circle2.transform.position, radius);
        Gizmos.DrawWireSphere(circleR.transform.position, radius);
        Gizmos.DrawWireSphere(circleR2.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }
    private IEnumerator dash()
    {
        if (!isEmirHoca)
        {
            canDash = false;
            isDashing = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * 9, 0f);
            yield return new WaitForSeconds(dashTime / 2);
            rb.gravityScale = 1f;
            isDashing = false;
        }
        else
        {
            if (isGrounded || isGrounded2)
            {
                canDash = false;
                isDashing = true;
                anim.SetBool("isRolling", true);
                rb.velocity = new Vector2(transform.localScale.x * 2.8f, rb.velocity.y);
                yield return new WaitForSeconds(dashTime * 1.6f);
                anim.SetBool("isRolling", false);
                isDashing = false;
            }
        }
    }

    public void block()
    {
        if (isEmirHoca && isGrounded && isGrounded2)
        {
            if (Input.GetMouseButton(1))
            {
                isBlocking = true;
                rb.velocity = Vector2.zero;
                anim.SetBool("isBlocking", true);
            }
            else
            {
                isBlocking = false;
                anim.SetBool("isBlocking", false);
            }
        }
    }

    /*public IEnumerator block()
    {
        isBlocking = true;
        anim.SetBool("isBlocking",true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.8f);
        isBlocking = false;
        canBlock = false;
        anim.SetBool("isBlocking", false);
        yield return new WaitForSeconds(3);
        canBlock = true;
    }
    private void blockCheck()
    {
        if (isEmirHoca && Input.GetMouseButtonDown(1) && canBlock && isGrounded&&isGrounded2)
        {
                StartCoroutine(block());
        }
    }*/

    void arrowCheck()
    {
        if (!isEmirHoca && Input.GetMouseButtonDown(1) && Time.time > nextShoot)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (!isFaceRight && (mousePosition - shootPointObj.transform.position).normalized.x > 0)
                Flip();
            else if (isFaceRight && (mousePosition - shootPointObj.transform.position).normalized.x < 0)
                Flip();
            Vector3 shootPoint = shootPointObj.transform.position;
            shootDirection = (mousePosition - shootPoint).normalized;
            Instantiate(arrow, shootPoint, Quaternion.identity);
            nextShoot = Time.time + 0.5f;
        }
    }
}