using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject testPanel;
    PlayerInput playerInput;
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
        playerInput = new PlayerInput();
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
                // Cursor.lockState = CursorLockMode.None;
                testPanel.SetActive(true);
                SwitchToUIInput();
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                // Cursor.lockState = CursorLockMode.None;
                testPanel.SetActive(false);
                SwitchToPlayerInput();
            }

        }
    }

    public void SwitchToUIInput()
    {
        // playerInput.SwitchCurrentActionMap("UI");
        playerInput.UI.Enable();
        playerInput.Player.Disable();
    }

    public void SwitchToPlayerInput()
    {
        // playerInput.SwitchCurrentActionMap("Player");
        playerInput.UI.Disable();
        playerInput.Player.Enable();
    }
}
