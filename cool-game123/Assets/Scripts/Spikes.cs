using UnityEngine;

public class Spike : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Get the player component
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
                anim.SetBool("isDead", true);
            }
        }
    }
}
