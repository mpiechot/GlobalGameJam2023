using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementsBubble : MonoBehaviour
{
    [SerializeField] private Image requirementsImage;
    [SerializeField] private Animator requirementsBubbleAnimator;

    public void ChangeRequirement(Sprite requirement)
    {
        requirementsImage.sprite = requirement;
        requirementsBubbleAnimator.SetTrigger("Show");
    }
}
