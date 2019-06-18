using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    public float mass = 1.0f;
    public static float globalGravity = -9.81f;
    public float? customGravity = null;
    public bool hasGravity = true;

    Rigidbody targetRB;

    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasGravity)
        {
            var grav = customGravity.HasValue ? customGravity.Value : globalGravity;

            Vector3 gravity = grav * mass * Vector3.up;
            targetRB.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
