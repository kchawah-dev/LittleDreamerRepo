using UnityEngine;

public class WallClimb : MonoBehaviour
{
    public float climbSpeed = 4f;
    public LayerMask wallLayer;
    private Rigidbody2D rb;
    private bool isTouchingWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isTouchingWall = Physics2D.OverlapCircle(transform.position, 0.3f, wallLayer);

        if (isTouchingWall && Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed);
        }
    }
}