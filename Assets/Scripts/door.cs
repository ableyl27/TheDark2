using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string requiredKeyTag;
    
    public bool CanOpen(GameObject heldKey)
    {
        return heldKey.CompareTag(requiredKeyTag);
        
    }

    public void Interact(PlayerInteractionController player)
    {
        if(player.isHoldingKey() && CanOpen(player.keyInHand))
        {
            Destroy(player.keyInHand);
            player.keyInHand = null;
            Open();
        }
        else
        {
            Debug.Log("need a key");
        }
    }

    public void Open()
    {
        Destroy(gameObject);

    }
}