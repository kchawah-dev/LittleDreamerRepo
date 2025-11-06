using UnityEngine;

public class EnemyDefeat : MonoBehaviour
{
    public GameObject deathEffect; // optional particle or animation

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy the enemy
            Destroy(transform.parent.gameObject);

            // Add a small bounce to the player
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f);

            // Optional: spawn effect
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
        }
    }
}