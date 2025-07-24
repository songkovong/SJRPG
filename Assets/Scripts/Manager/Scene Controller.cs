using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AsyncOperation LoadSceneAsync(string scene)
    {
        return SceneManager.LoadSceneAsync(scene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadGameScene()
    {
        LoadSceneAsync("Game Scene");
    }

    public void LoadMainScene()
    {
        Debug.Log("Game manager is = " + GameManager.instance);
        if (GameManager.instance != null)
        {
            GameManager.instance.SaveAll();
        }

        LoadSceneAsync("Main Scene");
    }
}
