using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when Start button is clicked
    public void PlayGame()
    {
        // Scene name must match exactly (including spaces and punctuation)
        SceneManager.LoadScene("Level 1-1");
    }

    // Called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Shows in the Unity Console
        Application.Quit(); // Works in builds
    }
}