using UnityEngine;
using TMPro;
using System.Collections;
public class PickupUI : MonoBehaviour
{
    [SerializeField] private GameObject pickupText;
    [SerializeField] private GameObject pickupUI;
    [SerializeField] private GameObject keyIndicator;

    [SerializeField] private GameObject chestOpenedPanel;
    [SerializeField] private GameObject wrongKeyPanel;

    private bool hasPickedUpKey = false;

    private void OnEnable()
    {
        PlayerInteractionController.OnItemPickedUp += ShowMessage;
        PlayerInteractionController.OnKeyPickedUp += ShowKeyIndicator;
        PlayerInteractionController.OnKeyDropped += HideKeyIndicator;
        PlayerInteractionController.OnKeyUsed += HideKeyIndicator;
        chest.OnChestOpened += ShowChestOpened;
        chest.OnWrongKey += ShowWrongKey;
    }

    private void OnDisable()
    {
        PlayerInteractionController.OnItemPickedUp -= ShowMessage;
        PlayerInteractionController.OnKeyPickedUp -= ShowKeyIndicator;
        PlayerInteractionController.OnKeyDropped -= HideKeyIndicator;
        PlayerInteractionController.OnKeyUsed -= HideKeyIndicator;
        chest.OnChestOpened -= ShowChestOpened;
        chest.OnWrongKey -= ShowWrongKey;
    }

    private void ShowChestOpened()
    {
        StopAllCoroutines();
        StartCoroutine(ShowPanelRoutine(chestOpenedPanel));
    }

    private void ShowWrongKey()
    {
        StopAllCoroutines();
        StartCoroutine(ShowPanelRoutine(wrongKeyPanel));
    }

    private IEnumerator ShowPanelRoutine(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        panel.SetActive(false);
    }

    private void ShowKeyIndicator()
    {
        keyIndicator.SetActive(true);
    }
    private void HideKeyIndicator()
    {
        keyIndicator.SetActive(false);
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
