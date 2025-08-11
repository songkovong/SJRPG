using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // SoundManager.Instance.Play2DSound("Game BGM", 0f, true, SoundType.BGM);

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Main Scene":
                SoundManager.Instance.Play2DSound("Main BGM", 0f, true, SoundType.BGM);
                break;
            case "Game Scene":
                SoundManager.Instance.Play2DSound("Game BGM", 0f, true, SoundType.BGM);
                break;
            default:
                break;
        }
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
        if (player != null)
        {
            player.playerStat.SaveAllData();
        }
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
