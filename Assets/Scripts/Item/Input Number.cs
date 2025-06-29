using System.Collections;
using TMPro;
using UnityEngine;

public class InputNumber : MonoBehaviour
{
    private bool activate = false;

    [SerializeField]
    private TMP_Text text_Preview;
    [SerializeField]
    private TMP_InputField text_input;

    [SerializeField]
    private GameObject go_Base;

    Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (activate)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OK();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }
        }
    }

    public void Call()
    {
        go_Base.SetActive(true);
        activate = true;
        text_input.text = "";
        text_Preview.text = DragSlot.instance.dragSlot.itemCount.ToString();
    }

    public void Cancel()
    {
        go_Base.SetActive(false);
        activate = false;
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.instance.SetColor(0);

        int num;
        if (text_input.text != "")
        {
            if (CheckNumber(text_input.text))
            {
                num = int.Parse(text_input.text);
                if (num > DragSlot.instance.dragSlot.itemCount)
                {
                    num = DragSlot.instance.dragSlot.itemCount;
                }
            }
            else
            {
                num = 1;
            }
        }
        else
        {
            num = int.Parse(text_Preview.text);
        }

        StartCoroutine(DropItemCoroutine(num));
    }

    IEnumerator DropItemCoroutine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            Instantiate(DragSlot.instance.dragSlot.item.itemPrefab,
                player.transform.position + player.transform.forward + player.transform.up,
                Quaternion.identity);

            DragSlot.instance.dragSlot.SetSlotCount(-1);

            yield return new WaitForSeconds(0.05f);
        }

        DragSlot.instance.dragSlot = null;
        go_Base.SetActive(false);
        activate = false;
    }

    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57)
            {
                continue;
            }
            isNumber = false;
        }
        Debug.Log("isNumber = " + isNumber);
        return isNumber;
    }
}
