using UnityEngine;
using TMPro;

public class InteractTooltipUI : MonoBehaviour
{
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private void OnEnable()
    {
        PlayerInteractionController.OnInteractableFound += UpdateTooltip;
    }

    private void OnDisable()
    {
        PlayerInteractionController.OnInteractableFound -= UpdateTooltip;
    }

    private void UpdateTooltip(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            tooltipPanel.SetActive(false);
        }
        else
        {
            tooltipPanel.SetActive(true);
            tooltipText.text = text;
        }
    }
}