using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsContainer : MonoBehaviour
{
    [SerializeField, Tooltip("Requirements a tree might have; e.g. water / fertilizer")]
    private Interactable[] requirementTypes;
    public Interactable[] RequirementTypes
    {
        get { return requirementTypes; }
    }

    void Start()
    {
        DeactivateRequirements();
    }

    public void DeactivateRequirements()
    {
        foreach (Interactable go in requirementTypes)
        {
            go.gameObject.SetActive(false);
        }
    }

}
