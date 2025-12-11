using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    public float speed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;
    public bool movingRight = true;
    public bool spriteFacesRight = true; // set to false if your art faces left when localScale.x is positive

    [Header("Death")]
    public GameObject deathEffect; // optional particle or animation
    public float stompBounce = 6f;
    public float ignoreCollisionDuration = 0.15f; // short time to avoid the player getting stuck when stomping

    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        initialScale = transform.localScale;
        ApplyFacing(movingRight);
    }

    void FixedUpdate()
    {
        if (rightPoint != null && leftPoint != null && leftPoint.position.x > rightPoint.position.x)
        {
            // Ensure leftPoint is actually left of rightPoint
            var temp = leftPoint;
            leftPoint = rightPoint;
            rightPoint = temp;
        }

        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            if (rightPoint != null && transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
                ApplyFacing(movingRight);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            if (leftPoint != null && transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
                ApplyFacing(movingRight);
            }
        }
    }

    void ApplyFacing(bool faceRight)
    {
        // Robust flipping: allow for sprites that are authored facing left or right.
        float baseX = Mathf.Abs(initialScale.x);
        float desired = faceRight ? 1f : -1f;
        if (!spriteFacesRight) desired = -desired;
        var s = initialScale;
        s.x = baseX * desired;
        transform.localScale = s;
    }

    // Trigger handler (if enemy collider is a trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        HandlePlayerContact(other, other.attachedRigidbody ?? other.GetComponent<Rigidbody2D>());
    }

    // Collision handler (if enemy collider is not a trigger)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        HandlePlayerContact(collision.collider, collision.rigidbody ?? collision.collider.attachedRigidbody);
    }

    private void HandlePlayerContact(Collider2D playerCollider, Rigidbody2D playerRb)
    {
        if (playerCollider == null || enemyCollider == null)
            return;

        // Heuristic stomp detection:
        // Player's bottom must be at or above enemy top (with a small margin) and player is moving downward or near zero vertical velocity.
        const float stompMargin = 0.08f;
        float playerBottom = playerCollider.bounds.min.y;
        float enemyTop = enemyCollider.bounds.max.y;

        bool fromAbove = false;
        if (playerRb != null)
        {
            fromAbove = playerBottom >= enemyTop - stompMargin && playerRb.linearVelocity.y <= 0.6f;
        }
        else
        {
            // fallback if no rigidbody: use center comparison
            fromAbove = playerCollider.bounds.center.y > enemyCollider.bounds.center.y;
        }

        if (fromAbove)
        {
            // Temporarily ignore collision so the player doesn't get stuck on the enemy's remains
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
            StartCoroutine(ReenableCollisionLater(playerCollider, enemyCollider, ignoreCollisionDuration));

            // Bounce the player up
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, stompBounce);
            }

            // Spawn optional effect
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            // Destroy the enemy object (destroy parent if present to preserve original behaviour)
            // Delay a tiny bit to ensure physics/overlap state is settled (optional)
            Destroy(transform.parent != null ? transform.parent.gameObject : gameObject, 0.02f);
        }
        else
        {
            // Hit from side or otherwise -> game over
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator ReenableCollisionLater(Collider2D a, Collider2D b, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (a != null && b != null)
        {
            Physics2D.IgnoreCollision(a, b, false);
        }
    }
}