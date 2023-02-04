using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fist : MonoBehaviour
{
    GameObject self;
    List<Rigidbody> targets = new List<Rigidbody>();


    public void Start()
    {
        if (!GetComponent<Collider>().isTrigger)
            Debug.LogError("Needs to be Trigger");
    }

    public void ExecuteHit(float stunDuration)
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].AddForce(transform.forward * 40, ForceMode.Impulse);
            Player enemyPlayer = targets[i].GetComponent<Player>();
            if (enemyPlayer)
            {
                enemyPlayer.Stun(stunDuration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other != self)
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                targets.Add(rigidbody);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other != self)
        {
            targets.Remove(other.GetComponent<Rigidbody>());
        }
    }
}
