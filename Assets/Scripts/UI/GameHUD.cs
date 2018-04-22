using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {

    [SerializeField] private PlayerManager player;
    [SerializeField] private TurnManager turnMgr;

    [SerializeField] private Text movePointText;
    [SerializeField] private Text stolenItemText;
    [SerializeField] private Text totalStolenValueText;
    [SerializeField] private Image lastTurnFeedback;
    [SerializeField] private Text turnFeedbackText;

    [SerializeField] private ItemPanelUI itemPanel;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = player.GetComponent<PlayerInventory>();

        player.OnDetected += FeedbackLastTurn;
        player.OnMoveDone += UpdateMovePoint;
        playerInventory.OnSteal += IncrementItemCount;

        turnMgr.OnPlayerTurn += FeedbackPlayerTurn;
        turnMgr.OnComputerTurn += FeedbackComputerTurn;

        UpdateTotalStolenValue(0);
    }

    private void UpdateMovePoint()
    {
        movePointText.text = player.CurrentMovePoint.ToString();
    }

    private void IncrementItemCount(Item item)
    {
        stolenItemText.text = playerInventory.ItemCount.ToString();

        UpdateTotalStolenValue(playerInventory.TotalStolenValue);

        itemPanel.DisplayItem(item);
    }

    private void UpdateTotalStolenValue(int newValue)
    {
        totalStolenValueText.text = FormatScore(newValue);
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


    private void FeedbackLastTurn()
    {
        lastTurnFeedback.color = Color.yellow;
        turnFeedbackText.color = Color.black;
        turnFeedbackText.text = "Last turn";
    }

    private void FeedbackPlayerTurn()
    {
        turnFeedbackText.text = "Your\nturn";
        UpdateMovePoint();
    }

    private void FeedbackComputerTurn()
    {
        turnFeedbackText.text = "Wait";
    }

    /// <summary>
    /// Force player turn to finish.
    /// </summary>
    public void OnPressFinishTurn()
    {
        turnMgr.OnPlayerTurnFinished();
    }
}
