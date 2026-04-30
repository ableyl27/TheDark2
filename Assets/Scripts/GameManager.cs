using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PlayerDamageController.OnPlayerDeath += HandleDeath;
    }
    void OnDisable()
    {
        PlayerDamageController.OnPlayerDeath -= HandleDeath;
    }

    void HandleDeath()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
