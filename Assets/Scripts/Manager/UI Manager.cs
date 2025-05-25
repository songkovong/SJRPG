using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    Player player;
    public GameObject statPanel;
    public GameObject itemPanel;
    bool stat = false;
    bool item = false;
    bool close = false;
    bool pausetime = false;
    public List<GameObject> floatingUI = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        // pausetime = stat || item;

        ShowStat();
        ShowItem();
        Pause();
        IsUIFloating();
        CloseUI();
    }

    void ShowStat()
    {
        if (player.StatPressed)
        {
            floatingUI[0].SetActive(true);
        }
        else
        {
            floatingUI[0].SetActive(false);
        }
    }

    void ShowItem()
    {
        if (player.ItemPressed)
        {
            floatingUI[1].SetActive(true);
        }
        else
        {
            floatingUI[1].SetActive(false);
        }
    }

    void CloseUI()
    {
        if (player.ClosePressed)
        {
            for (int i = 0; i < floatingUI.Count; i++)
            {
                if (floatingUI[i].activeSelf)
                {
                    floatingUI[i].SetActive(false);
                    // return;
                }
            }
            pausetime = false;
        }
    }

    void IsUIFloating()
    {
        for (int i = 0; i < floatingUI.Count; i++)
        {
            if (floatingUI[i].activeSelf)
            {
                pausetime = true;
            }
        }
    }
    
    void Pause()
    {
        if (pausetime)
        {
            GameManager.instance.PauseGame();
        }
        else
        {
            GameManager.instance.DePauseGame();
        }
    }

    
}
