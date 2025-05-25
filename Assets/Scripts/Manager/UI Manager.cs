using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    Player player;
    public GameObject statPanel;
    public GameObject itemPanel;
    public InputActionAsset inputActions;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        ShowStat();
        ShowItem();
        Pause();
    }

    void ShowStat()
    {
        if (player.StatPressed)
        {
            statPanel.SetActive(true);
            OpenUI();
        }
        else
        {
            statPanel.SetActive(false);
            CloseUI();
        }
    }

    void ShowItem()
    {
        if (player.ItemPressed)
        {
            itemPanel.SetActive(true);
            OpenUI();
        }
        else
        {
            itemPanel.SetActive(false);
            CloseUI();
        }
    }
    void Pause()
    {
        if (player.ClosePressed)
        {
            GameManager.instance.PauseGame();
        }
        else
        {
            GameManager.instance.DePauseGame();
        }
    }

    void OpenUI()
    {
        // inputActions.FindActionMap("Player").Disable();
        // inputActions.FindActionMap("UI").Enable();
        player.playerInput.Player.Disable();
        player.playerInput.UI.Enable();
    }

    void CloseUI()
    {
        // inputActions.FindActionMap("Player").Enable();
        // inputActions.FindActionMap("UI").Disable();
        player.playerInput.Player.Enable();
        player.playerInput.UI.Disable();
    }
}
