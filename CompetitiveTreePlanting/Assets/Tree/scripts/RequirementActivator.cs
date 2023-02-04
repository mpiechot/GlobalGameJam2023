using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequirementActivator : MonoBehaviour
{
    public UnityEvent<Requirement> OnRequirementActivated;

    [SerializeField, Tooltip("Ref. to the RequirementsContainer")]
    private RequirementsContainer requirementsContainer;

    public void OnRequirementTimerTimeout()
    {
        int rand = Random.Range(0, requirementsContainer.RequirementTypes.Length);
        Requirement newRequirement = requirementsContainer.RequirementTypes[rand];
        newRequirement.gameObject.SetActive(true);

        OnRequirementActivated?.Invoke(newRequirement);
    }
}
