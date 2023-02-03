using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequirementActivator : MonoBehaviour
{
    public UnityEvent OnRequirementActivated;

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

        OnRequirementActivated?.Invoke();
        //requirementTimer.Stop();

    }


}
