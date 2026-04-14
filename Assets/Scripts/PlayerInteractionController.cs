using UnityEngine;
using System;

public class PlayerInteractionController : MonoBehaviour
{
    public GameObject keyInHand; 

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
        //Debug.Log("Test Interaction");
        TryInteract();
    }

    public bool isHoldingKey()
    {
        return keyInHand != null;
    }

    private void TryInteract()
    {
        Collider hit = GetInteractable();
        if(hit == null) { return; }

        IInteractable interactable = hit.GetComponent<IInteractable>();
        if(interactable != null)
        {
            interactable.Interact(this);
        }
    }

    public void PickUpKey(GameObject key)
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