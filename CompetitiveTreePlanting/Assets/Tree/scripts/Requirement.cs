using Assets;
using System;
using UnityEngine;

[Serializable]
public class Requirement
{
    [field: SerializeField]
    public InteractableType Type { get; set; }

    [field: SerializeField]
    public Sprite Sprite { get; set; }
}
