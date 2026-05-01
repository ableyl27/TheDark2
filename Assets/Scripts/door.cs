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
                isLocked = false;
                //Debug.Log("Door unlocked");
            }
            else
            {
                //Debug.Log("Door is locked");
                return;
            }
        }
        Open();
        // if(player.isHoldingKey() && CanOpen(player.keyInHand))
        // {
        //     Destroy(player.keyInHand);
        //     player.keyInHand = null;
        //     Open();
        // }
        // else
        // {
        //     Debug.Log("need a key");
        // }
    }

    public string GetInteractText(PlayerInteractionController player)
    {
        if (isLocked)
        {
            if (CanOpen(player.keyInHand))
            {
                return "Press E to use key";
            }
            else
            {
                return "Locked (need key)";
            }
    
        }
        return "Press E to open door";
    }

    public void Open()
    {
        Destroy(gameObject);

    }
}