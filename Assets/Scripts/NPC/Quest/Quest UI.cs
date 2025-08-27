using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance { get; private set;} 

    public Transform activeQuestContent;
    public Transform completedQuestContent;
    public GameObject questItemPrefab;

    public TMP_Text detailTitle;
    public TMP_Text detailDesc;
    public TMP_Text detailRequirement;
    public TMP_Text detailReward;

    public Button exitButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        exitButton.onClick.AddListener(PanelExit);
    }

    public void ShowQuestDetail(QuestData data, QuestProgress progress)
    {
        detailTitle.text = data.questName;
        detailDesc.text = data.questDesc;

        string req = "- Requirement\n";
        if (data.targetEnemyCount > 0)
        {
            int cur = progress != null ? progress.currentEnemyCount : data.targetEnemyCount;
            req += $"{data.targetEnemyName}: {cur} / {data.targetEnemyCount}\n";
        }
        if (data.targetItem != null && data.targetItemCount > 0)
        {
            int cur = progress != null ? progress.currentItemCount : data.targetItemCount;
            req += $"{data.targetItem.itemName}: {cur} / {data.targetItemCount}\n";
        }
        detailRequirement.text = req;

        string reward = "- Reward\n";
        if (data.rewardItem != null)
        {
            reward += $"{data.rewardItem.itemName}\n";
        }
        if (data.rewardCoin > 0)
        {
            reward += $"{data.rewardCoin} gold\n";
        }
        if (data.rewardEXP > 0)
        {
            reward += $"{data.rewardEXP} exp\n";
        }
        detailReward.text = reward;
    }

    public void RefreshUI(Dictionary<int, QuestProgress> activeQuests, HashSet<int> completedQuests, List<QuestData> allQuests)
    {
        foreach (Transform child in activeQuestContent) Destroy(child.gameObject);
        foreach (Transform child in completedQuestContent) Destroy(child.gameObject);

        // 진행중 퀘스트
        foreach (var kvp in activeQuests)
        {
            QuestData data = allQuests.Find(q => q.questID == kvp.Key);
            if (data != null)
            {
                var obj = Instantiate(questItemPrefab, activeQuestContent);
                obj.GetComponent<QuestItemUI>().Setup(data, kvp.Value);
            }
        }

        // 완료된 퀘스트
        foreach (var questID in completedQuests)
        {
            QuestData data = allQuests.Find(q => q.questID == questID);
            if (data != null)
            {
                var obj = Instantiate(questItemPrefab, completedQuestContent);
                obj.GetComponent<QuestItemUI>().Setup(data, null);
            }
        }
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }
}
