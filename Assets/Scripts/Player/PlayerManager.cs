using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private PlayerMovement move;

    [SerializeField] private int movePointBase = 5;
    [SerializeField] private int bonusPointLastTurn = 2;

    private int currentMovePoint;

    public bool HasBeenDetected
    {
        get; private set;
    }
    public bool HasBeenCatched {
        get; private set;
    }

    public Delegates.SimpleDelegate OnTurnFinished;
    public Delegates.SimpleDelegate OnDetected;
    public Delegates.SimpleDelegate OnCatched;

    void Awake()
    {
        move.OnMoveSucceed += MoveSucceed;
    }

    public void StartNewTurn()
    {
        currentMovePoint = movePointBase;

        if(HasBeenDetected)
        {
            currentMovePoint += bonusPointLastTurn;
        }
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
    
    public void DetectPlayer()
    {
        if(HasBeenDetected)
        {
            return;
        }

        HasBeenDetected = true;

        if (OnDetected != null)
        {
            OnDetected();
        }
    }

    public void CatchPlayer()
    {
        if(HasBeenCatched)
        {
            return;
        }

        HasBeenCatched = true;

        if (OnCatched != null)
        {
            OnCatched();
        }
    }
}
