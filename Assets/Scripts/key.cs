using UnityEngine;

public class key : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractionController player)
    {
        player.PickUpKey(gameObject);
    }

    public string GetInteractText(PlayerInteractionController player)
    {
        return "Press E to pick up key";
    }
}