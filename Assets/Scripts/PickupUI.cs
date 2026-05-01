using UnityEngine;
using TMPro;
using System.Collections;
public class PickupUI : MonoBehaviour
{
    [SerializeField] private GameObject pickupText;
    [SerializeField] private GameObject pickupUI;

    private bool hasPickedUpKey = false;

    private void OnEnable()
    {
        PlayerInteractionController.OnItemPickedUp += ShowMessage;
    }

    private void OnDisable()
    {
        PlayerInteractionController.OnItemPickedUp -= ShowMessage;
    }

    private void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {

         pickupUI.SetActive(true);

        if (!hasPickedUpKey)
        {
            pickupText.SetActive(true);
            hasPickedUpKey = true;
        }

        yield return new WaitForSeconds(1.5f);
       
        pickupUI.SetActive(false);
        pickupText.SetActive(false);

    }

}
