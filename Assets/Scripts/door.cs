using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    public string requiredKeyTag;
    [SerializeField] private bool isLocked = true;
    private bool isOpening = false;

    [Header("Audio")]
    [SerializeField] private AudioClip unlockDoor;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioSource audioSource;

    [Header("Animation")]
    [SerializeField] private Animator doorAnimator;

    public bool CanOpen(GameObject heldKey)
    {
        if (heldKey == null)
            return false;
        return heldKey.CompareTag(requiredKeyTag);
    }

    public void Interact(PlayerInteractionController player)
    {
        if (isOpening) return;

        if (isLocked)
        {
            if (CanOpen(player.keyInHand))
            {
                Destroy(player.keyInHand);
                player.keyInHand = null;
                isLocked = false;
                audioSource.PlayOneShot(unlockDoor);
                isOpening = true;
                OpenDoor();
                player.UseKey();
            }
            else
            {
                audioSource.PlayOneShot(doorLocked);
            }
        }
        else
        {
            isOpening = true;
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        doorAnimator.SetBool("isOpen", true);
    }

    public string GetInteractText(PlayerInteractionController player)
    {
        if (isLocked)
        {
            if (CanOpen(player.keyInHand))
                return "press E to use key";
            else
                return "locked (need key)";
        }
        return "press E to open door";
    }
}