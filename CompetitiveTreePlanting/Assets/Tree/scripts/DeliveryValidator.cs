using UnityEngine;
using UnityEngine.Events;

public class DeliveryValidator : MonoBehaviour
{
    public UnityEvent OnDelivery;

    [SerializeField, Tooltip("Ref. to the requirements container")]
    private RequirementsContainer requirementsContainer;

    public void OnCollisionDetected(GameObject gameObject)
    {
        //todo: if (gameObject.GetComponent<Player>().)
        if(true)
        {
            OnDelivery?.Invoke();
        }
    }
}
