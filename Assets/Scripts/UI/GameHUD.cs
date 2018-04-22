using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {

    [SerializeField] private PlayerManager player;
    [SerializeField] private TurnManager turnMgr;

    [SerializeField] private Text movePointText;
    [SerializeField] private Text stolenItemText;
    [SerializeField] private Image lastTurnFeedback;
    [SerializeField] private Text turnFeedbackText;
    
    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = player.GetComponent<PlayerInventory>();

        player.OnDetected += FeedbackLastTurn;
        player.OnMoveDone += UpdateMovePoint;
        playerInventory.OnSteal += IncrementItemCount;

        turnMgr.OnPlayerTurn += FeedbackPlayerTurn;
        turnMgr.OnComputerTurn += FeedbackComputerTurn;
    }

    private void UpdateMovePoint()
    {
        movePointText.text = player.CurrentMovePoint.ToString();
    }

    private void IncrementItemCount(Item item)
    {
        stolenItemText.text = playerInventory.ItemCount.ToString();
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
}
