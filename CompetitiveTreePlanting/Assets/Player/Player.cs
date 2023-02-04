#nullable enable

using Assets;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;

    private Guid playerId;
    
    public Guid PlayerId => playerId;

    public Interactable? CarriedObject => playerInteraction.CarriedObject;

    private void Start()
    {
        playerId = Guid.NewGuid();
    }
}
