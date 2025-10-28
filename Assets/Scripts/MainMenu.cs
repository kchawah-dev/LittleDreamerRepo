using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when Start button is clicked
    public void PlayGame()
    {
        // Make sure the scene name matches your first level exactly
        SceneManager.LoadScene("Level1");
    }

    // Called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Only visible in Editor
        Application.Quit(); // Works in builds
    }
}