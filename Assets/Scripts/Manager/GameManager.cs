using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    Player player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        isPaused = false;

        Application.targetFrameRate = 120;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        // player = GameObject.FindWithTag("Player").GetComponent<Player>();
        // StartCoroutine(player.playerStat.AutoSaveRoutine()); // Auto Save
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void DePauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}
