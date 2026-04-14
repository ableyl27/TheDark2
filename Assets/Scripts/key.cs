using UnityEngine;

public class key : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractionController player)
    {
        player.PickUpKey(gameObject);
    }
}