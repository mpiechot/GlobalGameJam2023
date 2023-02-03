#nullable enable

using Assets;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CarryableObjects carryableObjects;

    private Guid playerId;
    private Interactable? carriedObject;

    public Guid PlayerId => playerId;

    public Interactable? CarriedObject => carriedObject;

    public void Interact(Vector3 direction)
    {
        Debug.Log("Interact with this shit!");
        Collider[] objectsToInteractWith = Physics.OverlapSphere(transform.position + transform.forward, 1f);

        foreach (Collider obj in objectsToInteractWith)
        {
            if (obj.transform.TryGetComponent<Interactable>(out var interactable))
            {
                CarryObject(interactable.Type);
                Debug.Log("Carry: " + interactable.Type);
                return;
            }
        }
    }

    private void CarryObject(InteractableType type)
    {
        if(carriedObject != null)
        {
            // We already carry something. We need to destroy it to carry something new.
            Destroy(carriedObject.gameObject);
        }

        switch (type) 
        {
            case InteractableType.WATER:
            {
                carriedObject = Instantiate(carryableObjects.waterPotPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
            }
            case InteractableType.FERTILIZER:
            {
                carriedObject = Instantiate(carryableObjects.fertilizerPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }

    private void Start()
    {
        playerId = Guid.NewGuid();
    }
}
