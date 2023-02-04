using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class MeshSelector : MonoBehaviour
{
    public int Mesh;
    public List<Mesh> meshes;
    private int lastMesh;


    private void Update()
    {
       if(lastMesh != Mesh && meshes.Count > 0)
        {
            GetComponent<MeshFilter>().mesh = meshes[Mathf.Max(0,Mathf.Min(meshes.Count, Mesh))];
        }
        
    }


}
