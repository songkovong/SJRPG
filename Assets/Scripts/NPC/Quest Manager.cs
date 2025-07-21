using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private HashSet<string> acceptedQuests = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AcceptQuest(string questID)
    {
        if (string.IsNullOrEmpty(questID)) return;

        if (!acceptedQuests.Contains(questID))
        {
            acceptedQuests.Add(questID);
        }
    }

    public bool HasQuest(string questID)
    {
        return acceptedQuests.Contains(questID);
    }
}
