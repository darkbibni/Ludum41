using UnityEngine;

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
