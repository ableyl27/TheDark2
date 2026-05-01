using UnityEngine;
using TMPro;
using System.Collections;
public class PickupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickupText;

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
        StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        pickupText.text = message;
        pickupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        pickupText.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
