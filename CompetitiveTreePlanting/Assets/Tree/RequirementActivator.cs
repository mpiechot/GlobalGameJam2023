using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementActivator : MonoBehaviour
{
    [SerializeField, Tooltip("Ref. to the RequirementsContainer")]
    private RequirementsContainer requirementsContainer;

    [SerializeField, Tooltip("Reference to the RequirementTimer")]
    private RandomTimer requirementTimer;

    public void OnRequirementTimerTimeout()
    {
        //requirementsContainer.DeactivateRequirements();

        int rand = Random.Range(0, requirementsContainer.RequirementTypes.Length);
        GameObject newRequirement = requirementsContainer.RequirementTypes[rand];
        newRequirement.SetActive(true);

        requirementTimer.Stop();
    }


}
