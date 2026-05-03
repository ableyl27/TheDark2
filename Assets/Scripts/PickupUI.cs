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
    [SerializeField] private GameObject specialKeyPanel;

    [SerializeField] private string requiredKeyName;

    private Coroutine specialKeyRoutine;
    private bool hasPickedUpKey = false;

    private enum UIState
    {
        None,
        Message,
        KeyIndicator,
        ChestOpened,
        WrongKey,
        SpecialKey
    }

    private UIState currentState = UIState.None;
    private Coroutine activeRoutine;

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


    private void RunUIRoutine(UIState newState, IEnumerator routine)
    {
        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        currentState = newState;
        activeRoutine = StartCoroutine(routine);
    }

    private IEnumerator ShowPanelRoutine(GameObject panel, UIState state, float duration)
    {
        panel.SetActive(true);

        yield return new WaitForSeconds(duration);

        panel.SetActive(false);

        if (currentState == state)
        {
            currentState = UIState.None;
        }
    }


    private void ShowChestOpened()
    {
        RunUIRoutine(
            UIState.ChestOpened,
            ShowPanelRoutine(chestOpenedPanel, UIState.ChestOpened, 3f)
        );
    }

    private void ShowWrongKey()
    {
        RunUIRoutine(
            UIState.WrongKey,
            ShowPanelRoutine(wrongKeyPanel, UIState.WrongKey, 3f)
        );
    }

    private void ShowMessage(string message)
    {
        RunUIRoutine(
            UIState.Message,
            ShowMessageRoutine(message)
        );
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        pickupUI.SetActive(true);

        if (!hasPickedUpKey)
        {
            pickupText.SetActive(true);
            hasPickedUpKey = true;
        }

        yield return new WaitForSeconds(3f);

        pickupUI.SetActive(false);
        pickupText.SetActive(false);

        currentState = UIState.None;
    }

    private void ShowSpecialKeyPanel(GameObject key)
    {
        if (key.name != requiredKeyName)
            return;

        if (specialKeyRoutine != null)
            StopCoroutine(specialKeyRoutine);

        specialKeyRoutine = StartCoroutine(SpecialKeyRoutine());
    }

    private IEnumerator SpecialKeyRoutine()
    {
        specialKeyPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        specialKeyPanel.SetActive(false);
        specialKeyRoutine = null;
    }

    private void ShowKeyIndicator(GameObject key)
    {
        wrongKeyPanel.SetActive(false);
        keyIndicator.SetActive(true);
        currentState = UIState.KeyIndicator;
        ShowSpecialKeyPanel(key);
    }

    private void HideKeyIndicator()
    {
        keyIndicator.SetActive(false);
        currentState = UIState.None;
    }
}