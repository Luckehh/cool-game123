using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDamage = 20;

    public float attackRate = 3f; // Cooldown time between attacks
    float nextAttackTime = 0f;

    public void AttackTrig()
    {
        anim.SetBool("Attack", true);
    }
    public void AttackAnim()
    {
        // Check if it's time for the enemy to attack based on cooldown
        if (Time.time >= nextAttackTime)
        {
            // Reset the cooldown for the next attack
            nextAttackTime = Time.time + 1f / attackRate;

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

            foreach (Collider2D playerCollider in hitPlayers)
            {
                Player player = playerCollider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(attackDamage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}