using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    public GameObject statPanel;
    public GameObject itemPanel;
    public GameObject skillPanel;
    public SlotTooltip itemTooltipPanel;

    InputNumber inputNumber;

    private Stack<GameObject> openWindows = new Stack<GameObject>();

    Player player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        inputNumber = GameObject.FindWithTag("Input Number").GetComponent<InputNumber>();
        itemTooltipPanel = GameObject.FindWithTag("Item Tooltip").GetComponent<SlotTooltip>();
    }

    void Start()
    {
        WindowInitialize();

        player.playerInput.UI.Stat.started += OnStat;
        player.playerInput.UI.Item.started += OnItem;
        player.playerInput.UI.Skill.started += OnSkill;
        player.playerInput.UI.Close.started += OnClose;

    }

    void Update()
    {
        IsAnyWindow();
    }

    void OnDestroy()
    {
        player.playerInput.UI.Stat.started -= OnStat;
        player.playerInput.UI.Item.started -= OnItem;
        player.playerInput.UI.Skill.started -= OnSkill;
        player.playerInput.UI.Close.started -= OnClose;
    }

    void OnStat(InputAction.CallbackContext ctx)
    {
        OpenWindow(statPanel);
    }

    void OnItem(InputAction.CallbackContext ctx)
    {
        OpenWindow(itemPanel);
    }

    void OnSkill(InputAction.CallbackContext ctx)
    {
        OpenWindow(skillPanel);
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
            TooltipClose(panel);
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
            TooltipClose(panel);
            openWindows = new Stack<GameObject>(openWindows.Where(p => p != panel).Reverse());
        }
    }

    void CloseLastWindow()
    {
        if (openWindows.Count > 0)
        {
            GameObject panel = openWindows.Pop();
            panel.SetActive(false);
            TooltipClose(panel);
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

    void WindowInitialize()
    {
        statPanel.SetActive(false);
        itemPanel.SetActive(false);
        skillPanel.SetActive(false);
    }

    void TooltipClose(GameObject panel)
    {
        if (panel == itemPanel)
        {
            itemTooltipPanel.HideTooltip();
            inputNumber.Cancel();
        }
        else if (panel == skillPanel)
        {
            TooltipController.instance?.HideTooltip();
        }
        else if (panel == statPanel)
        {
            StatTooltipController.instance?.HideTooltip();
        }
    }
}
