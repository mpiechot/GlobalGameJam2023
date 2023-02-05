using UnityEngine;
using UnityEngine.UI;

public class RequirementsBubble : MonoBehaviour
{
    [SerializeField] private Image requirementsImage;
    [SerializeField] private Animator requirementsBubbleAnimator;
    [SerializeField] private Sprite[] requirementSprites;

    public void ChangeRequirement(int requirement)
    {
        requirementsImage.sprite = requirementSprites[requirement];
        requirementsBubbleAnimator.SetTrigger("Show");
    }
}
