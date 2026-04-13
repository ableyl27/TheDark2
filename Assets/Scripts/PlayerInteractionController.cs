using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private GameObject key;
    
    [SerializedField] private float InteractionRadius;
    [SerializeField] private LayerMask interactableLayers;

    private Collider GetInteractable()
    {
       return Physics.OverlapSphere(transform.position, interactRadius, interactableLayers);

    }

    public static event Action<string> OnInteractableFound;

    private void OnInteract()
    {
        Debug.Log("Test Interaction")
        if(isHoldingKey())
        {
            DropKey();
            return;
        }
        TryPickUpKey();
    }

    private bool isHoldingKey()
    {
        return keyInHand != null;
    }

    private void TryPickUpKey()
        {
            //later
        }

    private void DropKey()
        {
            //later
        }
    
}
