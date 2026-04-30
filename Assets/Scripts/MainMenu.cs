using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("dark");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
