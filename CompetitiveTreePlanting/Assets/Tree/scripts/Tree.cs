using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the RequirementTimer")]
    private RandomTimer requirementTimer;

    [SerializeField, Tooltip("Reference to the GrowthTimer")]
    private Timer growthTimer;

    private Interactable requirement;

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

    public void SetRequirementForTree(Interactable requirement) 
    { 
        this.requirement = requirement;
        requirementTimer.Stop();
        growthTimer.Pause();
    }
}
