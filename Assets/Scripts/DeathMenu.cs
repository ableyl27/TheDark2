using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        restartButton.onClick.AddListener(RestartButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    void Update()
    {
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("dark");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Start");
    }
}