using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFistAnim : MonoBehaviour
{
    public void ExecuteHit()
    {
        GetComponent<Animator>().SetTrigger("Hit");
    }
}
