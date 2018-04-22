using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class WarningUI : MonoBehaviour {

    [SerializeField] PlayerManager player;

    [SerializeField] CanvasGroup warningImg;
    [SerializeField] float warningFadeDuration = 0.75f;
    [SerializeField] int animCount = 3;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        player.OnDetected += TriggerWarningAnim;
    }
    
	public void TriggerWarningAnim()
    {
        StartCoroutine(WarningAnim());

        audioSource.Play();
    }

    private IEnumerator WarningAnim()
    {
        Tween fade, unfade;

        for (int i = 0; i < animCount; i++)
        {
            fade = warningImg.DOFade(0.75f, warningFadeDuration * 0.5f).SetEase(Ease.Linear);
            yield return new WaitWhile(() => (fade.IsPlaying()));

            unfade = warningImg.DOFade(0f, warningFadeDuration * 0.5f).SetEase(Ease.Linear);
            yield return new WaitWhile(() => (unfade.IsPlaying()));
        }
    }
}
