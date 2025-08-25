using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public List<QuestData> allQuests;
    private Dictionary<int, QuestProgress> activeQuests = new Dictionary<int, QuestProgress>();
    private HashSet<int> completeQuests = new HashSet<int>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool AcceptQuest(int questID)
    {
        if (completeQuests.Contains(questID)) return false;

        if (activeQuests.ContainsKey(questID)) return false;

        activeQuests.Add(questID, new QuestProgress
        {
            questID = questID,
            currentEnemyCount = 0,
            currentItemCount = 0,
            isCompleted = false
        });

        return true;
    }

    public bool HasQuest(int questID)
    {
        return activeQuests.ContainsKey(questID);
    }

    public QuestProgress GetQuestProgress(int questID)
    {
        return activeQuests.ContainsKey(questID) ? activeQuests[questID] : null;
    }

    public void UpdateEnemyKill(int enemyCode)
    {
        var keys = new List<int>(activeQuests.Keys);
        foreach (var questID in keys)
        {
            QuestData quest = allQuests.Find(q => q.questID == questID);
            if (quest != null && quest.targetEnemyCode == enemyCode)
            {
                QuestProgress progress = activeQuests[questID];
                progress.currentEnemyCount++;
                CheckQuestComplete(quest, progress);
            }
        }
    }

    public void UpdateItemCollect(Item item)
    {
        var keys = new List<int>(activeQuests.Keys);
        foreach (var questID in keys)
        {
            QuestData quest = allQuests.Find(q => q.questID == questID);
            if (quest != null && quest.targetItem == item)
            {
                QuestProgress progress = activeQuests[questID];
                progress.currentItemCount++;
                CheckQuestComplete(quest, progress);
            }
        }
    }

    private void CheckQuestComplete(QuestData quest, QuestProgress progress)
    {
        if (progress.isCompleted) return;

        bool enemyDone = quest.targetEnemyCount <= progress.currentEnemyCount;
        bool itemDone = quest.targetItemCount <= progress.currentItemCount;

        if (enemyDone && itemDone)
        {
            progress.isCompleted = true;
            GiveReward(quest);
            activeQuests.Remove(quest.questID);
            completeQuests.Add(quest.questID);
            Debug.Log($"Quest Complete");
        }
    }

    private void GiveReward(QuestData quest)
    {
        // Debug.Log(quest.rewardCoin);
        if (quest.rewardItem != null)
        {
            Player.instance.inventory.AcquireItem(quest.rewardItem);
        }

        if (quest.rewardCoin != 0)
        {
            Player.instance.inventory.AcquireCoin(quest.rewardCoin);
        }
    }

    [System.Serializable]
    private class QuestSaveData
    {
        public List<QuestProgress> quests;
    }

    public void SaveQuests()
    {
        QuestSaveData saveData = new QuestSaveData
        {
            quests = new List<QuestProgress>()
        };

        saveData.quests.AddRange(activeQuests.Values);

        foreach (var questID in completeQuests)
        {
            saveData.quests.Add(new QuestProgress
            {
                questID = questID,
                currentEnemyCount = 0,
                currentItemCount = 0,
                isCompleted = true
            });
        }

        string json = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString("QuestSaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadQuests()
    {
        if (PlayerPrefs.HasKey("QuestSaveData"))
        {
            string json = PlayerPrefs.GetString("QuestSaveData");
            QuestSaveData saveData = JsonUtility.FromJson<QuestSaveData>(json);

            activeQuests.Clear();
            completeQuests.Clear();

            foreach (var progress in saveData.quests)
            {
                if (progress.isCompleted)
                {
                    completeQuests.Add(progress.questID);
                }
                else
                {
                    activeQuests.Add(progress.questID, progress);
                }
            }
        }
    }

    public void ResetAllQuests()
    {
        activeQuests.Clear();
        completeQuests.Clear();

        SaveQuests();
    }
}

[System.Serializable]
public class QuestProgress
{
    public int questID;
    public int currentEnemyCount;
    public int currentItemCount;
    public bool isCompleted;
}