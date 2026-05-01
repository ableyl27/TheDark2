using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string requiredKeyTag;

    [SerializeField] private bool isLocked = true;
    
    public bool CanOpen(GameObject heldKey)
    {
        if (heldKey == null)
        {
            return false;
        }
        return heldKey.CompareTag(requiredKeyTag);
        
    }

    public void Interact(PlayerInteractionController player)
    {
        if (isLocked)
        {
            if (CanOpen(player.keyInHand))
            {
                Destroy(player.keyInHand);
                player.keyInHand = null;
                isLocked = false;
            }
            else
            {
                return;
            }
        }
        Open();
    }

    public string GetInteractText(PlayerInteractionController player)
    {
        if (isLocked)
        {
            if (CanOpen(player.keyInHand))
            {
                return "press E to use key";
            }
            else
            {
                return "locked (need key)";
            }
    
        }
        return "press E to open door";
    }

    public void Open()
    {
        Destroy(gameObject);
        
    }
}