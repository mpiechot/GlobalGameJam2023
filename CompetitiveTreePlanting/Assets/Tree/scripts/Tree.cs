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
    private RequirementsBubble requirementsBubble;

    private Requirement requirement;

    public UnityEvent OnDelivery;

    private Guid assignedPlayerId;

    public Guid AssignedPlayerId => assignedPlayerId;

    public void Initialize(Guid assignedPlayer)
    {
        assignedPlayerId = assignedPlayer;
    }

    public bool FeedTree(InteractableType feedType)
    {
        if(feedType == requirement.Type)
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
}
