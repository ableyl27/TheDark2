using UnityEngine;
using System;

public class PlayerInteractionController : MonoBehaviour
{

    public GameObject keyInHand; 

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private AudioClip keyPickUpSound;
    [SerializeField] private AudioSource audioSource;

    

    public static event Action<string> OnInteractableFound;
    public static event Action<string> OnItemPickedUp;

    public static event Action OnKeyPickedUp;
    public static event Action OnKeyDropped;
    public static event Action OnKeyUsed;



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
        key.SetActive(false);
        OnKeyPickedUp?.Invoke();
        audioSource.PlayOneShot(keyPickUpSound);
        OnItemPickedUp?.Invoke("Key picked up! Press F to drop.");
    }

    private void DropKey()
    {
        keyInHand.transform.SetParent(null);
        keyInHand.SetActive(true);
        OnKeyDropped?.Invoke();
        keyInHand = null;
    }

    private void OnDrop()
    {
        if(isHoldingKey())
        {
            DropKey();
        }
    }

    public void UseKey()
    {
        OnKeyUsed?.Invoke();
    }

}