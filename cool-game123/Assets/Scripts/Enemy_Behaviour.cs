using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float attackCooldown = 3f; // Cooldown time between attacks
    float nextAttackTime = 0f;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inrange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public Enemy_Combat enemyCombat; // Reference to the Enemy_Combat script
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    #endregion
    void Awake()
    {
        Debug.Log("Awake() called.");
        anim = GetComponent<Animator>();
        anim.SetBool("canWalk", true);
        Debug.Log("canWalk set to true in Awake(): " + anim.GetBool("canWalk"));
        SelectTarget();
    }

    void Start()
    {
        Debug.Log("Start() called.");
        anim = GetComponent<Animator>();
        anim.SetBool("canWalk", true);
        Debug.Log("canWalk set to true in Start(): " + anim.GetBool("canWalk"));
        SelectTarget();
    }


    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inrange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            SelectTarget();
        }

        if (inrange)
        {
            EnemyLogic();
        }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
        }
    }

    void Move()
    {
        // Check if not in attack mode, not cooling down, and not in attack range
        if (!attackMode && !cooling && distance > attackDistance)
        {
            anim.SetBool("canWalk", true);
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("canWalk", false);
        }
    }




    void Attack()
    {
        // Check if it's time for the enemy to attack based on cooldown
        if (Time.time >= nextAttackTime)
        {
            // Reset the cooldown for the next attack
            nextAttackTime = Time.time + attackCooldown;

            // Call the AttackAnim method from the Enemy_Combat script
            enemyCombat.AttackAnim();

            // Set the "Attack" trigger in the Animator
            anim.SetTrigger("Attack");

            // Set the attack mode to true
            attackMode = true;

            // Start a coroutine to monitor the attack animation state
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        // Set cooling to true to prevent further attacks until cooldown ends
        cooling = true;

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Set cooling back to false after the attack animation finishes
        cooling = false;

        // Wait for the cooldown period
        yield return new WaitForSeconds(attackCooldown - anim.GetCurrentAnimatorStateInfo(0).length);

        // Set the attack mode to false
        attackMode = false;
    }


    void Cooldown()
    {
        if (Time.time >= nextAttackTime)
        {
            cooling = false;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    public void Flip()
    {
        if (enabled)
        {
            Vector3 rotation = transform.eulerAngles;
            if (transform.position.x > target.position.x)
            {
                rotation.y = 180f;
            }
            else
            {
                rotation.y = 0f;
            }
            transform.eulerAngles = rotation;
        }
    }
}