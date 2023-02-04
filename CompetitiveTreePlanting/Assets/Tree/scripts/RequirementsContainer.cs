using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsContainer : MonoBehaviour
{
    [SerializeField, Tooltip("Requirements a tree might have; e.g. water / fertilizer")]
    private Requirement[] requirementTypes;

    [SerializeField]
    private Animator requirementsBubbleAnimator;

    public Requirement[] RequirementTypes
    {
        get { return requirementTypes; }
    }

    void Start()
    {
        DeactivateRequirements();
    }

    public void DeactivateRequirements()
    {
        requirementsBubbleAnimator.SetTrigger("Hide");
    }
}
