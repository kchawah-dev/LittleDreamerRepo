using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    public static SceneMusicManager instance;

    [Header("Music Tracks")]
    public AudioClip titleMusic;
    public AudioClip levelMusic;
    public AudioClip victoryJingle;

    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 1f; // Adjustable volume between 0 (mute) and 1 (max)

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        PlayMusicForScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip newClip = null;
        bool shouldLoop = true;

        switch (sceneName)
        {
            case "Title Screen":
                newClip = titleMusic;
                shouldLoop = true;
                break;

            case "Level 1-1":
                newClip = levelMusic;
                shouldLoop = true;
                break;

            case "LevelComplete":
                newClip = victoryJingle;
                shouldLoop = false;
                break;
        }

        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.loop = shouldLoop;
            audioSource.volume = musicVolume; // Apply current volume
            audioSource.Play();
        }
    }

    private void Update()
    {
        // Update volume in real-time if changed in Inspector or via script
        audioSource.volume = musicVolume;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}