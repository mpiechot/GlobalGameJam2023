#nullable enable

using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InteractableType type;

    public InteractableType Type => type;
}
