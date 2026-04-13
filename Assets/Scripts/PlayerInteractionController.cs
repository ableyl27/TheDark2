using UnityEngine;
using System;

public class PlayerInteractionController : MonoBehaviour
{
    private GameObject keyInHand; 

    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactableLayers;

    public static event Action<string> OnInteractableFound;

    private Collider[] GetInteractable()
    {
        return Physics.OverlapSphere(transform.position, interactionRadius, interactableLayers);
    }

    private void OnInteract()
    {
        Debug.Log("Test Interaction");
        if (isHoldingKey())
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
        Collider hit = GetInteractable();
        if(hit == null)
        {
            Debug.Log("no object found");
            return;
        }
        Key key = hit.GetComponent<key>();
        if(key == null)
        {
            Debug.Log("no key found");
            return;
        }
       
       PickUpKey(hit.gameObject);

    }

    private void PickUpKey(GameObject key)
    {
        keyInHand = key;

        key.transform.SetParent(transform);
    }

    private void DropKey()
    {
        //later
    }
}