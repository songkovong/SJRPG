using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    CinemachineCamera cinemachineCamera;

    Player player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

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
