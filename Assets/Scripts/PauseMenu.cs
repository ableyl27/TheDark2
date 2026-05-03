using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Game State")]
    public bool isGameOver = false;

    void Start()
    {
        resumeButton.onClick.AddListener(ResumeButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    void Update()
    {
        if (isGameOver) return; // 🚨 BLOCK ALL PAUSE INPUT

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        bool isPaused = pauseMenu.activeSelf;

        pauseMenu.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeButton()
    {
        if (isGameOver) return;

        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1; // safety reset
        SceneManager.LoadScene("Start");
    }

    // Call this when player dies
    public void SetGameOver()
    {
        isGameOver = true;

        pauseMenu.SetActive(false);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}