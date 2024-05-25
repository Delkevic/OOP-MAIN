using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float attackTimer;
    [SerializeField]
    private float distance;
    private bool attackMode;
    public bool inRange;
    private bool cooling;
    private float intTimer;

    public Transform leftBound, rightBound;
    public GameObject actionZone,triggerZone;
    public Transform target;
    private Animator an;
    void Start()
    {
        
        intTimer = attackTimer;
        an = GetComponent<Animator>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackMode)
        {
            Move();
        }
        if (inRange)
        {
            EnemyBehavior();
        }
        if(!InsideOfBounds()&& !inRange && !an.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            SelectTarget();
        }
    }


    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position,leftBound.position);
        float distanceToRight = Vector2.Distance(transform.position,rightBound.position);
        if(distanceToLeft >distanceToRight)
        {
            target = leftBound;
        }
        else 
        {
            target = rightBound;
        }
        Flip();
    }
    public void StopAttack()
    {
        attackMode = false;
        an.SetBool("Attack", false);
        cooling = false;
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
    
    public void CoolDown()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && cooling && attackMode)
        {
            cooling = false;
            attackTimer = intTimer;
        }
    }
    public void EnemyBehavior()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (distance <= attackDistance && !cooling)
        {
            Attack();
        }
        if (cooling)
        {
            an.SetBool("Attack", false);
            CoolDown();
        }
    }

    public void Move()
    {
        an.SetBool("CanWalk",true);
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        attackTimer = intTimer;
        attackMode = true;
        an.SetBool("CanWalk", false);
        an.SetBool("Attack", true);
    }

    private bool InsideOfBounds()
    {
        return transform.position.x>leftBound.position.x && transform.position.x < rightBound.position.x;
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y =0f;
        }
        transform.eulerAngles = rotation;
    }
}