using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUp : MonoBehaviour
{
    public UnityEvent TreeGrownUp;

    private int level = 0;
    private GameObject currentTree = null;

    [SerializeField, Tooltip("Tree-State-FBX files, please order from low to tall.")]
    private GameObject[] treeLvls;

    // Start is called before the first frame update
    void Start()
    {
        PlaceTree();
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
        PlaceTree();
    }

    private void PlaceTree()
    {
        if(currentTree != null) Destroy(currentTree);
        GameObject newTree = Instantiate(treeLvls[level]);
        newTree.transform.parent = transform;
        newTree.transform.localPosition = Vector3.zero;
        currentTree = newTree;
    }

}
