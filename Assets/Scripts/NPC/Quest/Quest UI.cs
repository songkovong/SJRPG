using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Transform activeQuestContent;
    public Transform completedQuestContent;
    public GameObject questItemPrefab;

    public Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener(PanelExit);
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
