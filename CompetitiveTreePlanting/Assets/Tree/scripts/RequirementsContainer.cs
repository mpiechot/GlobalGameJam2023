using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsContainer : MonoBehaviour
{
    [SerializeField, Tooltip("Requirements a tree might have; e.g. water / fertilizer")]
    private Requirement[] requirementTypes;

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
        foreach (Requirement go in requirementTypes)
        {
            go.gameObject.SetActive(false);
        }
    }
}
