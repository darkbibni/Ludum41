using UnityEngine;

[System.Serializable]
public class ItemData : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public int rewardPrice;
    public string description;
}