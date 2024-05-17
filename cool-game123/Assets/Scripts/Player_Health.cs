using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    public void Die()
    {
        Debug.Log("Player Died");

        anim.SetBool("isDead", true);

        this.enabled = false;
        GetComponent<Player_Movement>().enabled = false;
        GetComponent<Player_Combat>().enabled = false;
    }
}
