using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleScreen : MonoBehaviour {

    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private float fadeDuration = 1.5f;

    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject credits;

    private bool isStarting;

    private void Awake()
    {
        credits.SetActive(false);
    }

    public void PlayGame()
    {
        if (!isStarting)
        {
            StartCoroutine(StartGame());
        }
    }

    public void ShowCredits()
    {
        titleScreen.SetActive(false);
        credits.SetActive(true);
    }

    public void UnshowCredits()
    {
        credits.SetActive(false);
        titleScreen.SetActive(true);
    }

    private IEnumerator StartGame()
    {
        isStarting = true;

        Tween t = fadeGroup.DOFade(1f, fadeDuration);

        yield return new WaitWhile(() => (t.IsPlaying()));

        SceneManager.LoadScene(1);
    }
}
