using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class BagUI : MonoBehaviour
{
    [SerializeField] private Image bagImg;
    [SerializeField] private Image itemStolenImg;
    [SerializeField] private Sprite[] newBagSizeSprites;

    [SerializeField] private Text stolenItemText;
    [SerializeField] private Text totalStolenValueText;

    private Vector3 originalPos;

    [SerializeField] private int[] sizeNeededToNextStep = { 5, 10 };
    private int currentStep;

    private void Awake()
    {
        currentStep = 0;
        originalPos = itemStolenImg.transform.localPosition;
    }

    public IEnumerator LootAnimation(Item item, int newItemCount)
    {
        Tween tween;

        itemStolenImg.sprite = item.Data.sprite;
        itemStolenImg.transform.localPosition = originalPos;
        tween = itemStolenImg.DOFade(1f, 0.5f);

        yield return new WaitWhile(() => (tween.IsPlaying()));

        tween = itemStolenImg.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(0.5f);
        itemStolenImg.DOFade(0f, 0f);

        // TODO Bag size update
        // Empty -> Small -> Big ?

        tween = bagImg.transform.DOScale(Vector3.one * 1.1f, 0.25f);
        IncrementStolenItemCount(newItemCount);

        yield return new WaitWhile(() => (tween.IsPlaying()));

        tween = bagImg.transform.DOScale(Vector3.one, 0.25f);
    }
    
    private void IncrementStolenItemCount(int newValue)
    {
        stolenItemText.text = newValue.ToString();
        
        if(currentStep < sizeNeededToNextStep.Length)
        {
            if (newValue >= sizeNeededToNextStep[currentStep])
            {
                bagImg.sprite = newBagSizeSprites[currentStep];
                currentStep++;
            }
        }
    }

    public void UpdateTotalStolenValue(int newValue)
    {
        totalStolenValueText.text = FormatScore(newValue) +" $";
    }

    /// <summary>
    /// Add space every threes characters in the string.
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public static string FormatScore(int score)
    {
        string scoreString = score.ToString();

        int i = 1;
        int spaceIndex = scoreString.Length - 4 * i + 1;

        while (spaceIndex > 0)
        {
            scoreString = scoreString.Insert(spaceIndex, " ");

            i++;
            spaceIndex = scoreString.Length - 4 * i + 1;
        }

        return scoreString;
    }
}
