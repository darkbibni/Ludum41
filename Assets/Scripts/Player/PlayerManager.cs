using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private PlayerMovement move;

    [SerializeField] private int movePointBase;

    private int currentMovePoint;

    public Delegates.SimpleDelegate OnTurnFinished;

    void Awake()
    {
        move.OnMoveSucceed += MoveSucceed;
    }

    public void StartNewTurn()
    {
        currentMovePoint = movePointBase;
    }

    void MoveSucceed()
    {
        currentMovePoint--;

        if(currentMovePoint <= 0)
        {
            FinishPlayerTurn();
        }
    }

    public void FinishPlayerTurn()
    {
        // TODO Some things...

        if(OnTurnFinished != null)
        {
            OnTurnFinished();
        }
    }
}
