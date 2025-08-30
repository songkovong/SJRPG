using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text acceptText;

    private DialogueData currentData;
    public List<string> currentLines;
    private int currentIndex = 0;
    private bool isPlaying = false;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool lineEnd = false;

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

            if (isTyping)
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                dialogueText.text = currentLines[currentIndex];
                isTyping = false;
                lineEnd = true;
                return;
            }

            if (lineEnd)
            {
                NextLine();
            }

        }
    }

    public void StartDialogue(string name, DialogueData data)
    {
        if (data == null) return;

        currentData = data;
        currentLines = data.GetDialogueLines();
        currentIndex = 0;
        isPlaying = true;
        dialoguePanel.SetActive(true);

        npcNameText.text = name;
        acceptText.text = "Next: E, Exit: Move";

        ShowLine();
    }

    private void ShowLine()
    {
        // if (currentData == null || currentIndex >= currentData.lines.Count)
        if (currentLines == null || currentIndex >= currentLines.Count)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = "";
        lineEnd = false;

        if(typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(currentLines[currentIndex]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;

        // string[] words = line.Split(' ');

        // for (int i = 0; i < words.Length; i++)
        // {
        //     dialogueText.text += words[i];

        //     if (i < words.Length - 1)
        //     {
        //         dialogueText.text += " ";
        //     }

        //     yield return new WaitForSeconds(0.1f);

        //     if (!isTyping) yield break;
        // }

        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];

            yield return new WaitForSeconds(0.05f);

            if (!isTyping) yield break;
        }

        isTyping = false;
        lineEnd = true;
    }

    private void NextLine()
    {
        currentIndex++;

        if (currentIndex < currentLines.Count)
        {
            if (currentIndex == currentLines.Count - 1 && (currentData.tirggerQuest || currentData.openShop))
            {
                acceptText.text = "Accept: E, Reject: Move";
            }
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
                bool accept = QuestManager.Instance.AcceptQuest(currentData.questID);
                if (accept) Debug.Log("Quest Accept");
                else Debug.Log("Quest is Complete or Accepted");
            }

            if (currentData.openShop)
            {
                ShopManager.Instance.OpenShop(currentData.shopID);
                UIManager.Instance.OpenWindow(UIManager.Instance.shopPanel);
            }
        }

        npcNameText.text = "";
        currentData = null;
        currentLines = null;
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }
}
