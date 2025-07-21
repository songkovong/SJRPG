using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private DialogueData currentData;
    private int currentIndex = 0;
    private bool isPlaying = false;

    Player player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        CloseDialogue();

        if (player == null)
        {
            player = Player.instance;
        }
    }

    void Update()
    {
        if (!isPlaying) return;
        if (player != null && player.EnteractionPressed)
        {
            player.EnteractionPressed = false;
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null) return;

        currentData = data;
        currentIndex = 0;
        isPlaying = true;
        dialoguePanel.SetActive(true);

        ShowLine();
    }

    private void ShowLine()
    {
        if (currentData == null || currentIndex >= currentData.lines.Count)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentData.lines[currentIndex];
    }

    private void NextLine()
    {
        currentIndex++;

        if (currentIndex < currentData.lines.Count)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        isPlaying = false;

        if (currentData != null)
        {
            if (currentData.tirggerQuest)
            {
                // Quest
            }

            if (currentData.openShop)
            {
                // Shop
            }
        }

        currentData = null;
        CloseDialogue();
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }
}
