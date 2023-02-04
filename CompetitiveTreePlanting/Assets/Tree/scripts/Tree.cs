using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
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
            return true;
        }
        return false;
    }

    public void SetRequirementForTree(Interactable requirement) 
    { 
        this.requirement = requirement;
    }
}
