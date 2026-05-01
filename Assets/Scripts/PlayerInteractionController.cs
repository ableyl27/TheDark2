using UnityEngine;
using System;

public class PlayerInteractionController : MonoBehaviour
{

    public GameObject keyInHand; 

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactableLayers;

    public static event Action<string> OnInteractableFound;
    public static event Action<string> OnItemPickedUp;

    private bool hasPickedUpKey = false;

    private IInteractable GetInteractable()
    {   
        Ray ray = new Ray(playerCamera.transform.position + playerCamera.transform.forward, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayers))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
            return hit.transform.GetComponentInParent<IInteractable>();
        }

        return null;

        // Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up * 1f, interactionRadius, interactableLayers);
        // return hits.Length > 0 ? hits[0] : null;
    }

    private void FixedUpdate()
    {
        
        //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.red);
        CheckForInteractable();
    }
    private IInteractable lastInteractable;
    private void CheckForInteractable()
    {
        IInteractable current = GetInteractable();

        if (current != lastInteractable)
        {
            lastInteractable = current;

            if (current != null)
            {
                //Debug.Log("INTERACTABLE: " + current.GetInteractText(this));
                OnInteractableFound?.Invoke(current.GetInteractText(this));
            }
            else
            {
                OnInteractableFound?.Invoke("");
            }
        }
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
        IInteractable interactable = GetInteractable();

        if (interactable != null)
        {
            if (isHoldingKey() && interactable is key)
                return;

            interactable.Interact(this);
        }
        
    }

    public void PickUpKey(GameObject key)
    {
        keyInHand = key;
        key.transform.SetParent(transform);
        if(!hasPickedUpKey)
        {
        OnItemPickedUp?.Invoke("Key picked up! Press F to drop.");
        hasPickedUpKey = true;
        }
        else
        {
            OnItemPickedUp?.Invoke("Key picked up!");
        }
    }

    private void DropKey()
    {
        keyInHand.transform.SetParent(null);
        keyInHand = null;
    }

    private void OnDrop()
    {
        if(isHoldingKey())
        {
            DropKey();
        }
    }

}