using UnityEngine;
using System;

public class chest : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string requiredKeyTag;

    [Header("Animation")]
    [SerializeField] private Animator chestAnimator;

    [Header("Audio")]
    [SerializeField] private AudioClip correctKeySound;
    [SerializeField] private AudioClip wrongKeySound;
    [SerializeField] private AudioSource audioSource;

    public static event Action OnChestOpened;
    public static event Action OnWrongKey;

    private bool isOpen = false;

    public void Interact(PlayerInteractionController player)
    {
        if (isOpen) return;

        if (player.keyInHand != null && player.keyInHand.CompareTag(requiredKeyTag))
        {
            isOpen = true;
            chestAnimator.SetBool("isOpen", true);
            Destroy(player.keyInHand);
            player.keyInHand = null;
            player.UseKey();
            audioSource.PlayOneShot(correctKeySound);
            OnChestOpened?.Invoke();
        }
        else
        {
            audioSource.PlayOneShot(wrongKeySound);
            OnWrongKey?.Invoke();        
        }
    }

    public string GetInteractText(PlayerInteractionController player)
    {
        if (isOpen) return "";

        if (player.keyInHand != null && player.keyInHand.CompareTag(requiredKeyTag))
            return "Press E to open chest";
            return "Locked (need key)";
    }
}
