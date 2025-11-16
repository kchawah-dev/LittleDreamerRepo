using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    public void LoadNextLevel()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel", "");

        switch (lastLevel)
        {
            case "Level 1-1":
                SceneManager.LoadScene("Level 1-2");
                break;
            case "Level 1-2":
                SceneManager.LoadScene("Level 1-3");
                break;
            case "Level 1-3":
                SceneManager.LoadScene("GameComplete");
                break;
            default:
                Debug.LogWarning("Unrecognized level: " + lastLevel);
                break;
        }
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}