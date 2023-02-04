using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private float deathThreshhold;

    private Guid playerId;
    private Tree playerTree;
    
    public Guid PlayerId => playerId;

    public Interactable? CarriedObject => playerInteraction.CarriedObject;

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

    public void Update()
    {
        if(transform.position.y < deathThreshhold)
        {
            Respawn();
        }
    }
}
