using Assets;
using UnityEngine;
using UnityEngine.UI;

public class TreeRequirements : MonoBehaviour
{
    [SerializeField] private Image requirementsImage;

    public void ChangeRequirement(Requirement requirement)
    {
        switch (requirement.Type)
        {
            case InteractableType.WATER:
                requirementsImage.color = Color.blue;
                break;
            case InteractableType.FERTILIZER:
                requirementsImage.color = Color.yellow;
                break;
            case InteractableType.TREE:
                requirementsImage.color = Color.white;
                break;
            default:
                break;
        }
    }
}
