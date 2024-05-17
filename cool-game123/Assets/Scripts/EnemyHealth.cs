using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
     
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy murdered");

        anim.SetBool("isDead", true);

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Enemy_Behaviour>().enabled = false;
    }

}