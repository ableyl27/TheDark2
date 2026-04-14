using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKeyTag;
    
    public bool CanOpen(GameObject heldKey)
    {
        return heldKey.CompareTag("key");
    }

    public void Open()
    {
        Destroy(gameObject);

    }
}