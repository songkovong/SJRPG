using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    public GameObject pausePanel;
    public GameObject playerPrefab;
    CinemachineCamera cinemachineCamera;

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

        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }

        isPaused = false;

        Application.targetFrameRate = 120;
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        FindPlayerAndCamera();

        // StartCoroutine(player.playerStat.AutoSaveRoutine()); // Auto Save
    }

    // Update is called once per frame
    void Update()
    {
        // FindPlayerAndCamera();
        StartCoroutine(AutoSave());
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

    public void SaveAll()
    {
        player.playerStat.SaveAllData();
    }

    public void LoadAll()
    {
        player.playerStat.LoadAllData();
    }

    public void DeleteAll()
    {
        player.playerStat.DeleteAllData();
    }

    void OnApplicationQuit()
    {
        player.playerStat.SaveAllData();
    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            SaveAll();
            Debug.Log("Auto Save");
        }
    }

    public void FindPlayerAndCamera()
    {
        player = Player.instance;
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();

        if (cinemachineCamera != null && player != null)
        {
            cinemachineCamera.Follow = player.transform;
            cinemachineCamera.LookAt = player.transform;
        }
    }

}
