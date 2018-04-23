using UnityEngine;

using DG.Tweening;

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

    public void Disappear()
    {
        spriteRenderer.DOFade(0f, 0.5f).onComplete = OnDissapearFinished;
    }

    private void OnDissapearFinished()
    {
        gameObject.SetActive(false);
    }
}
