using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequirementActivator : MonoBehaviour
{
    public UnityEvent<Interactable> OnRequirementActivated;

    [SerializeField, Tooltip("Ref. to the RequirementsContainer")]
    private RequirementsContainer requirementsContainer;

    [SerializeField, Tooltip("Reference to the RequirementTimer")]
    private RandomTimer requirementTimer;

    public void OnRequirementTimerTimeout()
    {
        //requirementsContainer.DeactivateRequirements();

        int rand = Random.Range(0, requirementsContainer.RequirementTypes.Length);
        Interactable newRequirement = requirementsContainer.RequirementTypes[rand];
        newRequirement.gameObject.SetActive(true);

        OnRequirementActivated?.Invoke(newRequirement);
        //requirementTimer.Stop();
    }


}
