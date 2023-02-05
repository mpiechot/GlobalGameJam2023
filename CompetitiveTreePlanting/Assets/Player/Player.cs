using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject stunIndicator;
    [SerializeField] Animator animator;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private float deathThreshhold;

    private Guid playerId;
    private Tree playerTree;

    private float stunStart;
    private float stunDuration;

    public Guid PlayerId => playerId;

    public Interactable? CarriedObject => playerInteraction.CarriedObject;

    private void OnEnable()
    {
        playerInteraction.OnPickUp += PickUp;
        playerInteraction.OnDrop += Drop;
    }

    private void OnDisable()
    {
        playerInteraction.OnPickUp -= PickUp;
        playerInteraction.OnDrop -= Drop;
    }




    public void Initialize()
    {
        playerId = Guid.NewGuid();
    }

    public void SetTree(Tree tree)
    {
        playerTree = tree;
    }

    public void Respawn()
    {
        transform.position = playerTree.transform.position + Vector3.back;
    }

    private void Drop()
    {
        animator.SetBool("Holding", false);
    }

    private void PickUp()
    {
        animator.SetBool("Holding", true);
    }

    public void Start()
    {
        stunIndicator.SetActive(false);
    }

    public void Update()
    {
        if(transform.position.y < deathThreshhold)
        {
            Respawn();
        }

        UpdateStun();
    }



    public bool Stunned { get; private set; } = false;

    public void Stun(float duration)
    {
        if (!Stunned)
        {
            Debug.Log("Stun: " + duration + "s");
            Stunned = true;
            stunStart = Time.time;
            stunDuration = duration;
            animator.SetBool("Stunned", true);
            stunIndicator.SetActive(true);
        }
    }


    private void UpdateStun()
    {
        Debug.Log(Stunned);

        if (Stunned)
        {
            if (stunStart + stunDuration < Time.time)
            {

                DisableStun();
            }
        }
    }

    private void DisableStun()
    {
        Debug.Log("Disable Stun");
        animator.SetBool("Stunned", false);
        Stunned = false;
        stunIndicator.SetActive(false);
    }
                
}
