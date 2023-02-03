using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsContainer : MonoBehaviour
{
    [SerializeField, Tooltip("Requirements a tree might have; e.g. water / fertilizer")]
    private GameObject[] requirementTypes;
    public GameObject[] RequirementTypes
    {
        get { return requirementTypes; }
    }

    void Start()
    {
        DeactivateRequirements();
    }

    public void DeactivateRequirements()
    {
        foreach (GameObject go in requirementTypes)
        {
            go.SetActive(false);
        }
    }

}
