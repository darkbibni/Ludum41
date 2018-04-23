using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {
    
    [SerializeField] private PlayerManager player;
    [SerializeField] private TurnManager turnMgr;

    [SerializeField] private Text movePointText;

    [SerializeField] private ItemPanelUI itemPanel;
    [SerializeField] private BagUI bagUI;
    [SerializeField] private TurnFeedback turnFeedback;

    private PlayerInventory playerInventory;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInventory = player.GetComponent<PlayerInventory>();
        playerInput = player.GetComponent<PlayerInput>();

        player.OnMoveDone += UpdateMovePoint;
        playerInventory.OnSteal += LootAnItem;

        playerInput.OnPressFinishTurn += OnPressFinishTurn;

        turnMgr.OnPlayerTurn += FeedbackPlayerTurn;
        turnMgr.OnComputerTurn += FeedbackComputerTurn;
        
        bagUI.UpdateTotalStolenValue(0);
    }

    private void UpdateMovePoint()
    {
        movePointText.text = player.CurrentMovePoint.ToString();
    }

    private void LootAnItem(Item item)
    {
        // Todo Queue for display !

        StartCoroutine(WaitLootAnim(item));
    }

    private IEnumerator WaitLootAnim(Item item)
    {
        itemPanel.DisplayItem(item);
        yield return bagUI.LootAnimation(item);

        bagUI.IncrementStolenItemCount(playerInventory.ItemCount);
        bagUI.UpdateTotalStolenValue(playerInventory.TotalStolenValue);
    }

    private void FeedbackLastTurn()
    {
        turnFeedback.LastTurnFeedback();
    }

    private void FeedbackPlayerTurn(bool isLastTurn)
    {
        if(isLastTurn)
        {
            turnFeedback.LastTurnFeedback();
        }

        else
        {
            turnFeedback.PlayerTurn();
        }
        
        UpdateMovePoint();
    }

    private void FeedbackComputerTurn()
    {
        turnFeedback.EnnemiesTurn();
    }

    /// <summary>
    /// Force player turn to finish.
    /// </summary>
    public void OnPressFinishTurn()
    {
        turnMgr.OnPlayerTurnFinished();
    }
}
