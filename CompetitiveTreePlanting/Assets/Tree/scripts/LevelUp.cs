using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUp : NetworkBehaviour
{
    public UnityEvent TreeGrownUp;

    private int level = 0;
    private GameObject currentTree = null;

    [SerializeField, Tooltip("Tree-State-FBX files, please order from low to tall.")]
    private GameObject[] treeLvls;

    // Start is called before the first frame update
    void Start()
    {
        RPC_PlaceTree(level);
    }

    public void OnLevelUp()
    {
        if (level < treeLvls.Length - 1)
        {
            level++;
        }
        else
        {
            TreeGrownUp?.Invoke();
        }

        if (Object.HasInputAuthority)
        {
            RPC_PlaceTree(level);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_PlaceTree(int received_level, RpcInfo info = default)
    {
        if(currentTree != null) Destroy(currentTree);
        GameObject newTree = Instantiate(treeLvls[received_level]);
        newTree.transform.parent = transform;
        newTree.transform.localPosition = Vector3.zero;
        currentTree = newTree;
    }

}
