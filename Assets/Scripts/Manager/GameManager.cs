using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    public GameObject pausePanel;
    public GameObject playerPrefab;
    CinemachineCamera cinemachineCamera;

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

        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }

        isPaused = false;

        Application.targetFrameRate = 120;
        Screen.SetResolution(1920, 1080, true);

        var player = Instantiate(playerPrefab);
        // cinemachineCamera = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();

        if (cinemachineCamera != null && player != null)
        {
            cinemachineCamera.Follow = player.transform;
            cinemachineCamera.LookAt = player.transform;
        }

    }

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
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void DePauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
