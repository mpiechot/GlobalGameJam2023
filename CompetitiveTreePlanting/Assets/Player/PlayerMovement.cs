using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float speed = 1;
    [SerializeField] float dashForce = 1;
    [SerializeField] float attackCooldown = 1;
    [SerializeField] float dashCooldown = 1;
    [SerializeField] UnityEvent<Vector3> tryInteract = new UnityEvent<Vector3>();

    private Vector2 moveVector = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private GGJInputActions inputControls;
    private Coroutine performHit;
    private Coroutine performDash;
    private float currentHitCooldown = 0f;
    private float currentDashCooldown;
    private bool dashPerformed = true;

    // Start is called before the first frame update
    private void Awake()
    {
        inputControls = new GGJInputActions();
        inputControls.Player.Interact.performed += _ => Interact();
        inputControls.Player.Hit.performed += _ => TryHit();
        inputControls.Player.Dash.performed += _ => TryDash();
        inputControls.Player.Movement.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        inputControls.Player.Movement.canceled += ctx => moveVector = Vector2.zero;
    }

    private IEnumerator Dash()
    {
        // Perform hit here!
        dashPerformed = false;

        currentDashCooldown = Time.time;
        while (Time.time - currentDashCooldown < dashCooldown)
        {
            yield return null;
        }

        performDash = null;
    }
    private void TryDash()
    {
        if (performDash != null)
        {
            return;
        }
        performDash = StartCoroutine(Dash());
    }

    private void TryHit()
    {
        if(performHit != null)
        {
            return;
        }
        performHit = StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        // Perform hit here!
        Debug.Log("Hit!");

        currentHitCooldown = Time.time;
        while(Time.time - currentHitCooldown < attackCooldown)
        {
            yield return null;
        }

        performHit = null;
    }

    private void Interact()
    {
        tryInteract.Invoke(new Vector3(direction.x, 0, direction.y));
    }

    private void FixedUpdate()
    {
        if (Math.Abs(moveVector.magnitude) > .1f)
        {
            Debug.Log("Move!");
            rigidBody.AddForce(new Vector3(moveVector.x * speed, 0, moveVector.y * speed), ForceMode.Impulse);
            direction = moveVector;
        }
        if(!dashPerformed) 
        {
            Debug.Log("Dash!");
            rigidBody.AddForce(new Vector3(direction.x * dashForce, 0, direction.y * dashForce), ForceMode.Impulse);
            dashPerformed = true;
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
