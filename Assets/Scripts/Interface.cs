public interface IInteractable
{
    void Interact(PlayerInteractionController player);
    string GetInteractText(PlayerInteractionController player);
}