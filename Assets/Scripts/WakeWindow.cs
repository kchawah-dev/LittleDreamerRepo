using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WakeWindow : MonoBehaviour
{
    [Header("Portal Settings")]
    public bool changeScene = false;
    public string targetSceneName;
    public Transform targetLocation; // used if staying in the same scene

    [Header("Teleport Settings")]
    public float cooldown = 1f; // prevents infinite teleport loop
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return; // wait for cooldown
        if (!other.CompareTag("Player")) return;

        if (changeScene && !string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else if (targetLocation != null)
        {
            StartCoroutine(Teleport(other));
        }
        else
        {
            Debug.LogWarning("No destination set for Wake Window portal!");
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        canTeleport = false;

        // Move the player instantly
        player.transform.position = targetLocation.position;

        // Prevent target window from firing immediately
        WakeWindow targetWindow = targetLocation.GetComponent<WakeWindow>();
        if (targetWindow != null)
        {
            targetWindow.canTeleport = false;
            targetWindow.StartCoroutine(targetWindow.ResetCooldown());
        }

        // Reset this portal after delay
        yield return new WaitForSeconds(cooldown);
        canTeleport = true;
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canTeleport = true;
    }
}