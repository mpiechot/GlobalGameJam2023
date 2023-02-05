using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class RequirementsBubble : MonoBehaviour
{
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

    public void ChangeRequirement(Sprite requirement)
    {
        image.sprite = requirement;
        shadowImage.sprite = requirement;
        Open();
    }
}
