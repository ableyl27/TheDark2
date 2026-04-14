using UnityEngine;
using System;

public class PlayerInteractionController : MonoBehaviour
{
    private GameObject keyInHand; 

    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactableLayers;

    public static event Action<string> OnInteractableFound;

    private Collider GetInteractable()
    {
    
    Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayers);
    return hits.Length > 0 ? hits[0] : null;
    
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

        Door door = hit.GetComponent<Door>();
        if(door != null)
        {
            if(isHoldingKey() && door.CanOpen(keyInHand))
            {
                door.Open();
                return;
            }
            Debug.Log("need a key");
            return;
        }

        key foundKey = hit.GetComponent<key>();
        if(foundKey == null)
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