using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementActivator : MonoBehaviour
{
    [SerializeField, Tooltip("Requirements a tree might have; e.g. water / fertilizer")]
    private GameObject[] requirementTypes;

    [SerializeField, Tooltip("Reference to the RequirementTimer")]
    private RandomTimer requirementTimer;

    void Start()
    {
        DeactivateRequirements();
    }

    public void OnRequirementTimerTimeout()
    {
        Debug.Log("BAM");
        DeactivateRequirements();

        int rand = Random.Range(0, requirementTypes.Length);
        GameObject newRequirement = requirementTypes[rand];
        newRequirement.SetActive(true);

        requirementTimer.Stop();
    }

    private void DeactivateRequirements()
    {
        foreach(GameObject go in requirementTypes)
        {
            go.SetActive(false);
        }
    }

}
