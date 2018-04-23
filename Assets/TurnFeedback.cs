using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnFeedback : MonoBehaviour {
    
    [SerializeField] private Image lastTurnFeedback;
    [SerializeField] private Text turnFeedbackText;

    [Header("Colors")]
    [SerializeField] Color playerTurnColor;
    [SerializeField] Color lastTurnColor;
    [SerializeField] Color ennemiesTurnColor;

    [Header("Texts to display")]
    [SerializeField] string playerTurnText;
    [SerializeField] string lastTurnText;
    [SerializeField] string ennemiesTurnText;
    
    public void PlayerTurn()
    {
        lastTurnFeedback.color = playerTurnColor;
        turnFeedbackText.text = playerTurnText;
    }

    public void LastTurnFeedback()
    {
        lastTurnFeedback.color = lastTurnColor;
        turnFeedbackText.text = lastTurnText;
    }

    public void EnnemiesTurn()
    {
        lastTurnFeedback.color = ennemiesTurnColor;
        turnFeedbackText.text = ennemiesTurnText;
    }
}
