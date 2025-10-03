using UnityEngine;

public class Grapple : MonoBehaviour
{
    public float swingForce = 10f;
    private DistanceJoint2D joint;
    private LineRenderer line;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 10f, LayerMask.GetMask("GrapplePoint"));

            if (hit.collider != null)
            {
                joint.connectedAnchor = hit.point;
                joint.enabled = true;

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
            line.enabled = false;
        }

        if (line.enabled)
        {
            line.SetPosition(0, transform.position);
        }
    }
}