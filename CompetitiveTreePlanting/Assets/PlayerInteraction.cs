#nullable enable

using Assets;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] CarryableObjects? carryableObjects;
    [SerializeField] Transform carryObjectLocation;
    [SerializeField] Player player;

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

    private void CarryObject(InteractableType type, GameObject interactedObject)
    {
        if (carryableObjects == null)
        {
            throw new InvalidOperationException($"Serialized Field {nameof(carryableObjects)} was not set in the inspector!");
        }

        if (carriedObject != null && type != InteractableType.TREE)
        {
            // We already carry something and we don't interact with the tree. We need to destroy it to carry something new.
            Destroy(carriedObject.gameObject);
        }

        switch (type)
        {
            case InteractableType.WATER:
                {
                    carriedObject = Instantiate(carryableObjects.waterPotPrefab, carryObjectLocation.position, Quaternion.identity, carryObjectLocation);
                    break;
                }
            case InteractableType.FERTILIZER:
                {
                    carriedObject = Instantiate(carryableObjects.fertilizerPrefab, carryObjectLocation.position, Quaternion.identity, carryObjectLocation);
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
                        Destroy(carriedObject.gameObject);
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
