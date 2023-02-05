using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float lateralDrag;
    [SerializeField] [Range(0, 1)] private float verticalDrag;


    private Rigidbody rb;
    private Vector3 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentVelocity = rb.velocity;
        currentVelocity.x *= 1 - lateralDrag;
        currentVelocity.z *= 1 - lateralDrag;
        currentVelocity.y *= 1 - verticalDrag;
        rb.velocity = currentVelocity;
    }
}
