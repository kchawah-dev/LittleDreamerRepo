using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Goes back to the Level Select screen
    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // Retries the previously loaded level
    public void RetryLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
}