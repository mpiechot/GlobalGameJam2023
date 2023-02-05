using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class RequirementsBubble : MonoBehaviour
{
    [SerializeField] private Image requirementsImage;
    [SerializeField] private Animator requirementsBubbleAnimator;
    [SerializeField] private Sprite[] requirementSprites;
    [SerializeField] private Image image;
    [SerializeField] private Image shadowImage;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Close();
    }

    public void Close()
    {
        animator.SetTrigger("Hide");
    }

    public void Open()
    {
        animator.SetTrigger("Show");

    }

    public void ChangeRequirement(int requirement)
    {
        requirementsImage.sprite = requirementSprites[requirement];
        requirementsBubbleAnimator.SetTrigger("Show");
        image.sprite = requirement;
        shadowImage.sprite = requirement;
        Open();
    }
}
