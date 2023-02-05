using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUp : NetworkBehaviour
{
    public UnityEvent<int> TreeGrownUp;

    private int level = 0;
    private GameObject currentTree = null;

    [SerializeField, Tooltip("Tree-State-FBX files, please order from low to tall.")]
    private GameObject[] treeLvls;

    [SerializeField, Tooltip("Ref. to the requirementActivator.")]
    private RequirementActivator requirementActivator;

    [SerializeField, Tooltip("Ref. to the tree component.")]
    private Tree tree;

    // Start is called before the first frame update
    void Start()
    {
        RPC_PlaceTree(level);

        requirementActivator.OnRequirementActivated.AddListener(PauseTreeAnimation);
        tree.OnDelivery.AddListener(PlayTreeAnimation);
    }

    public void OnLevelUp()
    {
        if (level < treeLvls.Length - 1)
        {
            level++;
        }
        else
        {
            TreeGrownUp?.Invoke(level);
        }

        RPC_PlaceTree(level);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_PlaceTree(int received_level, RpcInfo info = default)
    {
        if(currentTree != null) Destroy(currentTree);
        GameObject newTree = Instantiate(treeLvls[received_level]);
        newTree.transform.parent = transform;
        newTree.transform.localPosition = Vector3.zero;
        currentTree = newTree;

        

    }

    private void PauseTreeAnimation(Requirement requirement)
    {
        currentTree.GetComponent<Animation>().Stop();
    }

    private void PlayTreeAnimation()
    {
        currentTree.GetComponent<Animation>().Play();
    }

}
