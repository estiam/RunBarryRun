using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHelper : MonoBehaviour
{
    private float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public bool IsGrounded
    {
        get {
            RaycastHit hit;
            return Physics.Raycast(transform.position, -Vector2.up, out hit, distToGround);
        }
    }
}
