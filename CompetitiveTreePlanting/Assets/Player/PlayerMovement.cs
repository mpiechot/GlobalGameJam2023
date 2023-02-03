using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float speed = 1;

    private Vector2 moveVector = Vector2.zero;
    private GGJInputActions inputControls;

    // Start is called before the first frame update
    void Awake()
    {
        inputControls = new GGJInputActions();
        inputControls.Player.Interact.performed += _ => Interact();
        inputControls.Player.Movement.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        inputControls.Player.Movement.canceled += ctx => moveVector = Vector2.zero;
    }

    private void Interact()
    {
        Debug.Log("Interact with this shit!");
    }

    private void FixedUpdate()
    {
        if (Math.Abs(moveVector.magnitude) > .1f)
        {
            Debug.Log("Move!");
            rigidBody.AddForce(new Vector3(moveVector.x * speed, 0, moveVector.y * speed), ForceMode.Impulse);
        }
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}
