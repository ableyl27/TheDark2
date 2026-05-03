using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private string requiredKeyTag;

    private void OnEnable()
    {
        PlayerInteractionController.OnKeyPickedUp += CheckKey;
    }

    private void OnDisable()
    {
        PlayerInteractionController.OnKeyPickedUp -= CheckKey;
    }

    private void CheckKey(GameObject pickedKey)
    {
        if (pickedKey.CompareTag(requiredKeyTag))
        {
            Obj.SetActive(true);
        }
    }
}
