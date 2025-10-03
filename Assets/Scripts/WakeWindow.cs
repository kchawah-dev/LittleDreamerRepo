using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeWindow : MonoBehaviour
{
    [Header("Portal Settings")]
    public bool changeScene = false;
    public string targetSceneName;
    public Transform targetLocation; // used if staying in the same scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (changeScene && !string.IsNullOrEmpty(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else if (targetLocation != null)
            {
                other.transform.position = targetLocation.position;
            }
            else
            {
                Debug.LogWarning("No destination set for Wake Window portal!");
            }
        }
    }
}