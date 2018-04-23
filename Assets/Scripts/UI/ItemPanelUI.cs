using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class ItemPanelUI : MonoBehaviour {

    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayDuration = 1f;

    [SerializeField] private CanvasGroup panel;

    [SerializeField] private Text itemName;
    [SerializeField] private Text description;

    private bool isDisplaying;

    private Tween actualFade;
    private Coroutine actualFadeCoroutine;

    private void Awake()
    {
        panel.alpha = 0f;
    }

    public void DisplayItem(Item item)
    {
        if(isDisplaying)
        {
            actualFade.Kill();
            StopCoroutine(actualFadeCoroutine);
        }

        ItemData data = item.Data;

        itemName.text = data.itemName + " (" + data.rewardPrice + " $)";
        description.text = data.description;

        actualFadeCoroutine = StartCoroutine(FadeAndUnfade());
    }

    private IEnumerator FadeAndUnfade()
    {
        isDisplaying = true;

        actualFade = panel.DOFade(0.8f, fadeDuration);

        yield return new WaitWhile(() => (actualFade.IsPlaying()));

        yield return new WaitForSeconds(displayDuration);

        actualFade = panel.DOFade(0f, fadeDuration);
        yield return new WaitWhile(() => (actualFade.IsPlaying()));

        isDisplaying = false;
        actualFadeCoroutine = null;
        actualFade = null;
    }
}
