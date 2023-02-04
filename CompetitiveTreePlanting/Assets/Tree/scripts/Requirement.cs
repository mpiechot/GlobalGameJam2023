using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requirement : MonoBehaviour
{
    [SerializeField] private InteractableType type;

    public InteractableType Type => type;
}
