using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class RequirementsBubble : MonoBehaviour
{
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
        image.sprite = requirementSprites[requirement];
        shadowImage.sprite = requirementSprites[requirement];

        animator.SetTrigger("Show");
        Open();
    }
}
