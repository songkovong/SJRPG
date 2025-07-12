using UnityEngine;
using UnityEngine.SceneManagement;

public class TestNextScene : MonoBehaviour
{
    public void NextScene()
    {
        GameManager.instance.SaveAll();
        SceneManager.LoadScene("SampleScene");
    }
}
