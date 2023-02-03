using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Guid playerId;
    private GameObject carriedObject;

    public Guid PlayerId => playerId;

    public GameObject CarriedObject => carriedObject;

    public void Interact(Vector3 direction)
    {
        Debug.Log("Interact with this shit!");
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out var hit, 10))
        {
            if (hit.transform.tag.Equals("Interactable"))
            {
                carriedObject = hit.transform.gameObject;
            }
        }
    }

    private void Start()
    {
        playerId = Guid.NewGuid();
    }
}
