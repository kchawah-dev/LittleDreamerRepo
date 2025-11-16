using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public void LoadLevel1_1()
    {
        SceneManager.LoadScene("Level 1-1");
    }

    public void LoadLevel1_2()
    {
        SceneManager.LoadScene("Level 1-2");
    }

    public void LoadLevel1_3()
    {
        SceneManager.LoadScene("Level 1-3");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }
}