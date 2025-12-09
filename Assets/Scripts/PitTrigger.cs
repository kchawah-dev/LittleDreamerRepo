using UnityEngine;
using UnityEngine.SceneManagement;

public class PitTrigger : MonoBehaviour
{
    [Header("Game Over Settings")]
    public string gameOverScene = "GameOver";

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            Debug.Log("Player fell into pit! Loading Game Over screen...");

            // Save the current level as the LastLevel (so Retry works)
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            // Freeze player movement before loading screen
            var rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            // Load Game Over screen
            SceneManager.LoadScene(gameOverScene);
        }
    }
}