using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    public static SceneMusicManager instance;

    [Header("Music Tracks")]
    public AudioClip titleMusic;
    public AudioClip levelSelectMusic;   // Level Select screen
    public AudioClip desertLevelMusic;   // Level 1-1
    public AudioClip oceanLevelMusic;    // Level 1-2
    public AudioClip spaceLevelMusic;    // Level 1-3
    public AudioClip victoryJingle;      // USED BY LevelComplete & GameComplete
    public AudioClip failureJingle;      // GameOver jingle

    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 1f;

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
                break;

            case "LevelSelect":
                newClip = levelSelectMusic;
                break;

            case "Level 1-1":
                newClip = desertLevelMusic;
                break;

            case "Level 1-2":
                newClip = oceanLevelMusic;
                break;

            case "Level 1-3":
                newClip = spaceLevelMusic;
                break;

            // BOTH completion screens use the SAME victory jingle
            case "LevelComplete":
            case "GameComplete":
                newClip = victoryJingle;
                shouldLoop = false;
                break;

            // Game Over uses Failure Jingle
            case "GameOver":
                newClip = failureJingle;
                shouldLoop = false;
                break;

            default:
                newClip = null;
                break;
        }

        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.loop = shouldLoop;
            audioSource.volume = musicVolume;
            audioSource.Play();
        }
    }

    private void Update()
    {
        audioSource.volume = musicVolume;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}