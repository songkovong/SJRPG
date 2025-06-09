using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject statPanel;
    public GameObject itemPanel;

    private Stack<GameObject> openWindows = new Stack<GameObject>();

    Player player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        IsAnyWindow();
    }

    void OnEnable()
    {
        player.playerInput.UI.Stat.started += OnStat;
        player.playerInput.UI.Item.started += OnItem;
        player.playerInput.UI.Close.started += OnClose;
    }

    void OnDisable()
    {
        player.playerInput.UI.Stat.started -= OnStat;
        player.playerInput.UI.Item.started -= OnItem;
        player.playerInput.UI.Close.started -= OnClose;
    }

    void OnStat(InputAction.CallbackContext ctx)
    {
        Debug.Log("Stat");
        OpenWindow(statPanel);
    }

    void OnItem(InputAction.CallbackContext ctx)
    {
        OpenWindow(itemPanel);
    }

    void OnClose(InputAction.CallbackContext ctx)
    {
        CloseLastWindow();
    }

    void OpenWindow(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            openWindows = new Stack<GameObject>(openWindows.Where(p => p != panel).Reverse());
        }
        else
        {
            panel.SetActive(true);
            openWindows.Push(panel);
        }
    }

    public void CloseWindow(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            openWindows = new Stack<GameObject>(openWindows.Where(p => p != panel).Reverse());
        }
    }

    void CloseLastWindow()
    {
        if (openWindows.Count > 0)
        {
            GameObject panel = openWindows.Pop();
            panel.SetActive(false);
        }
    }

    void IsAnyWindow()
    {
        if (openWindows.Count > 0)
        {
            player.playerInput.Player.Disable();
            player.DontRotate = true;
        }
        else
        {
            player.playerInput.Player.Enable();
            player.DontRotate = false;
        }
    }
}
