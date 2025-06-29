using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    [Tooltip("ONLY HP, MP, DAMAGE")]
    public string[] parts;
    public int[] nums;
}