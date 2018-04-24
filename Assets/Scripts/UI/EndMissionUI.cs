using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMissionUI : MonoBehaviour {
    
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private Text endVerdictText;
    [SerializeField] private Text endCommentText;
    
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    [Header("Texts")]
    [SerializeField] private string winVerdict;
    [SerializeField] private string loseVerdict;
    [SerializeField] private string winComment = "You successfully stolen ";
    [SerializeField] private string loseComment = "You lose all your bounty";
    
    [SerializeField] private MissionManager missionMgr;
    [SerializeField] private PlayerInventory inventory;

    [SerializeField] private Transform groupLayout;
    [SerializeField] private ItemPreviewUI itemPrefab;

    private void Awake()
    {
        endPanel.SetActive(false);

        missionMgr.OnWin += FeedbackWin;
        missionMgr.OnLose += FeedbackLose;
    }

    private void FeedbackWin()
    {
        endVerdictText.color = winColor;
        endVerdictText.text = winVerdict;
        endCommentText.text = winComment + "\n" +BagUI.FormatScore(inventory.TotalStolenValue) + " $";

        SetupEndPanel();
    }

    private void FeedbackLose()
    {
        endVerdictText.color = loseColor;
        endVerdictText.text = loseVerdict;
        endCommentText.text = loseComment;

        SetupEndPanel();
    }

    private void SetupEndPanel()
    {
        GameUI.SetActive(false);
        endPanel.SetActive(true);

        StartCoroutine(DisplayItems());
    }

    private IEnumerator DisplayItems()
    {
        print(inventory.Items);

        foreach(Item item in inventory.Items)
        {
            ItemPreviewUI itemPreview = Instantiate(itemPrefab, groupLayout.transform);
            itemPreview.SetupPreview(item);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
