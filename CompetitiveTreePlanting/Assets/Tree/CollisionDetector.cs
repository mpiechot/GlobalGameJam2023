using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DetectionEvent : UnityEvent<GameObject>
{
}

public class CollisionDetector : MonoBehaviour
{
    public DetectionEvent collisionDetected;

    [SerializeField, Tooltip("Player owning this tree")]
    private GameObject assignedPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if(assignedPlayer.gameObject == collision.gameObject)
        {
            collisionDetected?.Invoke(collision.gameObject);
        }
    }

}
