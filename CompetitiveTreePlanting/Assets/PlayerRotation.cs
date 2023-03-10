using Fusion;
using MultiplayerDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : NetworkBehaviour
{

    public Transform avatar;
    public float angleSpeed = 1;

    //private GGJInputActions inputControls;

    float t;
    Quaternion fromRotation;
    Quaternion toRotation;
    float fromToAngle;

    public void Awake()
    {
        //inputControls = new GGJInputActions();
        //inputControls.Player.Movement.performed += ctx => SetRotation(ctx.ReadValue<Vector2>());
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.direction.magnitude > 0.5f)
                RPC_SetRotation(data.direction);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SetRotation(Vector2 movement)
    {
        t = 0;
        fromRotation = avatar.rotation;
        toRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y), Vector3.up);
        fromToAngle = Quaternion.Angle(fromRotation, toRotation);
    }

    private void OnEnable()
    {
        //inputControls.Enable();
    }

    private void OnDisable()
    {
        //inputControls.Disable();
    }

    public void Update()
    {
        t += Time.deltaTime / fromToAngle * angleSpeed;
        Quaternion nextRotation = Quaternion.Slerp(fromRotation, toRotation, t);
        avatar.rotation = nextRotation;
    }
}
