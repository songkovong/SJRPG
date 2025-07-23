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
        LoadSceneAsync("SampleScene");
    }

    public void LoadMainScene()
    {
        LoadSceneAsync("Main Scene");
    }
}
