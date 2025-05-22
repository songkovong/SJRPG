using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject testPanel;
    public bool isPaused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPaused = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                testPanel.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                testPanel.SetActive(false);
            }

        }
    }
}
