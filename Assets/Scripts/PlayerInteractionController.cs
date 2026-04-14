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
        TryInteract();
    }

    private bool isHoldingKey()
    {
        return keyInHand != null;
    }

    private void TryInteract()
    {
        Collider hit = GetInteractable();
        if(hit == null)
        {
            if(isHoldingKey())
            {
                DropKey();
            }
            else
            {
                Debug.Log("no object found");
            }
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
        keyInHand.transform.SetParent(null);
        keyInHand = null;
    }
}