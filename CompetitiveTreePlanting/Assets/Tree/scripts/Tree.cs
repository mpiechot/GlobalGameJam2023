using Assets;
using Fusion;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Tree : NetworkBehaviour
{
    [SerializeField, Tooltip("Reference to the RequirementTimer")]
    private RandomTimer requirementTimer;

    [SerializeField, Tooltip("Reference to the GrowthTimer")]
    private Timer growthTimer;

    [SerializeField]
    private ParticleSystem ownerParticles;

    [SerializeField]
    private RequirementsBubble requirementsBubble;

    [SerializeField, Tooltip("Player can be set manually for testing purposes!")]
    private Player owner;

    private Requirement requirement;

    public UnityEvent OnDelivery;

    private Guid assignedPlayerId;

    public Guid AssignedPlayerId => assignedPlayerId;


    public void Initialize(Guid assignedPlayer)
    {
        assignedPlayerId = assignedPlayer;
    }

    public void Start()
    {
        if (owner)
        {
            this.SetOwner(owner);
        }
    }


    public Player Owner { get { return owner; } }

    public void SetOwner(Player player)
    {
        owner = player;
        var main = ownerParticles.main;

        Color color = player.Color;
        color.a = 0.5f;
        main.startColor = color;
    }

    public bool FeedTree(InteractableType feedType)
    {
        if(requirement != null && feedType == requirement.Type)
        {
            OnDelivery?.Invoke();
            requirementTimer.Run();
            growthTimer.Unpause();
            return true;
        }
        return false;
    }

    public void SetRequirementForTree(Requirement requirement) 
    {
            this.requirement = requirement;
            switch (requirement.Type)
            {
                case InteractableType.WATER: RPC_SetRequirementForTree(0); break;
                case InteractableType.FERTILIZER: RPC_SetRequirementForTree(1); break;
                default: break;
            }         
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SetRequirementForTree(int requirement, RpcInfo info = default)
    {
        requirementTimer.Stop();
        growthTimer.Pause();
        requirementsBubble.ChangeRequirement(requirement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
