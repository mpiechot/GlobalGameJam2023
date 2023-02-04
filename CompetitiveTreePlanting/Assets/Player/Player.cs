using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private float deathThreshhold;

    private Guid playerId;
    private Tree playerTree;
    
    public Guid PlayerId => playerId;

    public Interactable? CarriedObject => playerInteraction.CarriedObject;

    private void OnEnable()
    {
        playerInteraction.OnPickUp += PickUp;
        playerInteraction.OnDrop += Drop;
    }

    private void OnDisable()
    {
        playerInteraction.OnPickUp -= PickUp;
        playerInteraction.OnDrop -= Drop;
    }



    public void Initialize()
    {
        playerId = Guid.NewGuid();
    }

    public void SetTree(Tree tree)
    {
        playerTree = tree;
    }

    public void Respawn()
    {
        transform.position = playerTree.transform.position + Vector3.back;
    }

    private void Drop()
    {
        animator.SetBool("Holding", false);
    }

    private void PickUp()
    {
        animator.SetBool("Holding", true);
    }

    public void Update()
    {
        if(transform.position.y < deathThreshhold)
        {
            Respawn();
        }
    }
}
