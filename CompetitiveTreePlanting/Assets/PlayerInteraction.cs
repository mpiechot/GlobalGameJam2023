#nullable enable

using Assets;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public delegate void Interaction();
    public event Interaction? OnPickUp;
    public event Interaction? OnDrop;


    [SerializeField] CarryableObjects? carryableObjects;
    [SerializeField] Transform? carryObjectLocation;
    [SerializeField] Player? player;

    private Interactable? carriedObject;

    public Interactable? CarriedObject => carriedObject;

    public void Interact(Vector3 direction)
    {
        Collider[] objectsToInteractWith = Physics.OverlapSphere(transform.position + transform.forward, 1f);

        foreach (Collider obj in objectsToInteractWith)
        {
            if (obj.transform.TryGetComponent<Interactable>(out var interactable))
            {
                CarryObject(interactable.Type, interactable.gameObject);
                return;
            }
        }
    }

    private void Drop()
    {
        if (carriedObject != null)
        {
            Destroy(carriedObject.gameObject);
            OnDrop.Invoke();
        }
    }

    private void PickUp(Interactable prefab) 
    {
        carriedObject = Instantiate(prefab, carryObjectLocation.position, carryObjectLocation.transform.rotation, carryObjectLocation);
        OnPickUp.Invoke();
    }

    private void CarryObject(InteractableType type, GameObject interactedObject)
    {
        if (carryableObjects == null)
        {
            throw new InvalidOperationException($"Serialized Field {nameof(carryableObjects)} was not set in the inspector!");
        }

        if (carriedObject != null && type != InteractableType.TREE)
        {
            // We already carry something and we don't interact with the tree. We need to destroy it to carry something new.
            Drop();
        }

        switch (type)
        {
            case InteractableType.WATER:
                {
                    PickUp(carryableObjects.waterPotPrefab);
                      break;
                }
            case InteractableType.FERTILIZER:
                {
                    PickUp(carryableObjects.fertilizerPrefab);
                    break;
                }
            case InteractableType.TREE:
                {
                    if(!interactedObject.TryGetComponent<Tree>(out var tree))
                    {
                        throw new InvalidCastException("Tried to interact with a tree, but there is no tree?!");
                    }

                    if(tree.AssignedPlayerId != player.PlayerId)
                    {
                        // This is not the players tree, so ignore it.
                        return;
                    }

                    if (carriedObject != null && tree.FeedTree(carriedObject.Type))
                    {
                        Drop();
                    }

                    break;
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }
}
