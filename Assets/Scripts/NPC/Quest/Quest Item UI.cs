using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestItemUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text questNameText;

    private QuestData _data;
    private QuestProgress _progress;


    public void Setup(QuestData data, QuestProgress progress)
    {
        this._data = data;
        this._progress = progress;

        questNameText.text = data.questName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            QuestUI.Instance.ShowQuestDetail(_data, _progress);
        }
    }
}
