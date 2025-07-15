using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaLoader : MonoBehaviour
{
    public string sceneNameToLoad;

    private bool isLoaded = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isLoaded && other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isLoaded && other.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(sceneNameToLoad);
            isLoaded = false;
        }
    }
}