using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public Sprite sprite;
    public string name;
    public string description;
}

public class Item : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteRenderer;

    private ItemData data;
    public ItemData Data
    {
        get { return data; }
        set {
            data = value;
            spriteRenderer.sprite = data.sprite;
        }
    }
}
