using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGate : MonoBehaviour
{
    [Header("Level Completion Settings")]
    public string levelCompleteScene = "LevelComplete"; // The scene that shows the "Level Complete" message

    private bool levelFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelFinished)
        {
            levelFinished = true;
            Debug.Log("Level Complete! Loading Level Complete screen...");

            // Save the current level name as LastLevel
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();  // optional but recommended

            // Freeze player movement
            var rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            // Load the Level Complete scene
            SceneManager.LoadScene(levelCompleteScene);
        }
    }
}