using Fusion;
using MultiplayerDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1;
    [SerializeField] private float dashForce = 1;
    [SerializeField] private float dashPushbackForce = 0.3f;
    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private float dashCooldown = 1;
    [SerializeField] private UnityEvent<Vector3> tryInteract = new UnityEvent<Vector3>();


    private Vector2 moveVector = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    //private GGJInputActions inputControls;
    private Coroutine performHit;
    private Coroutine performDash;
    private float currentHitCooldown = 0f;
    private float currentDashCooldown;
    private bool dashPerformed = true;
    private new Rigidbody rigidbody;

    Vector3 dashFrom;
    Vector3 dashTo;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        //inputControls = new GGJInputActions();
        //inputControls.Player.Interact.performed += _ => Interact();
        //inputControls.Player.Hit.performed += _ => TryHit();
        //inputControls.Player.Dash.performed += _ => TryDash();
        //inputControls.Player.Movement.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        //inputControls.Player.Movement.canceled += ctx => moveVector = Vector2.zero;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            moveVector = data.direction.normalized;
        }
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

        BroadcastMessage("ExecuteHit");
        animator.SetTrigger("Hit");

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
            rigidbody.AddForce(new Vector3(moveVector.x * speed, 0, moveVector.y * speed), ForceMode.Impulse);
            direction = moveVector;
        }
        if(!dashPerformed) 
        {
            rigidbody.AddForce(new Vector3(direction.x * dashForce, 0, direction.y * dashForce), ForceMode.Impulse);
            dashPerformed = true;
        }
    }

    public void Update()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude/8);
    }

    private void OnEnable()
    {
        //inputControls.Enable();
    }

    private void OnDisable()
    {
        //inputControls.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(dashFrom, dashTo);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Player")
        {

            Rigidbody other = collision.gameObject.GetComponent<Rigidbody>();
            if (other)
            {

                Vector3 contact = collision.GetContact(0).point;
                contact.y = 0;
                Vector3 playerPosition = transform.position;
                playerPosition.y = 0;

                Vector3 direction = contact - playerPosition;

                float dirMultiplier =  (float)(Math.Cos(Vector3.Angle(direction.normalized, rigidbody.velocity.normalized) / 180 * Math.PI));
                Vector3 propagatedForce = direction * dashPushbackForce * Math.Abs(dirMultiplier) * rigidbody.velocity.magnitude;      
                other.AddForce(Vector3.ClampMagnitude(propagatedForce, dashPushbackForce * 10), ForceMode.Impulse);

                dashFrom = contact;
                dashTo = contact + propagatedForce;
            }
        }
    }
}
