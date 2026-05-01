using UnityEngine;
using TMPro;

public class InteractTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltipText;

    private void OnEnable()
    {
        PlayerInteractionController.OnInteractableFound += UpdateTooltip;
    }

    private void OnDisable()
    {
        PlayerInteractionController.OnInteractableFound -= UpdateTooltip;
    }

    private void UpdateTooltip(string message)
    {
        tooltipText.text = message;
        tooltipText.gameObject.SetActive(!string.IsNullOrEmpty(message));
    }
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //Debug.Log("Tooltip UI script is alive"); 
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
