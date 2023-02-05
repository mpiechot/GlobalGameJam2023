using Assets;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
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
        requirementTimer.Stop();
        growthTimer.Pause();
        requirementsBubble.ChangeRequirement(requirement.Sprite);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
