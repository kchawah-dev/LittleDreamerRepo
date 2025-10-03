using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Animator animator; // Reference to Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get Animator component
    }

    void Update()
    {
        Move();
        Jump();
        // UpdateAnimator(); // No longer needed for jump trigger
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }

    void Jump()
    {
        if (groundCheck == null) return; // Safety check

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("Jump"); // Use trigger
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;
                animator.SetTrigger("Jump"); // Use trigger
            }
        }
    }
}
