using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    [SerializeField] PlayerManager player;

    public Delegates.SimpleDelegate OnWin;
    public Delegates.SimpleDelegate OnLose;

    public bool IsGameOver
    {
        get; set;
    }

    private void Awake()
    {
        IsGameOver = false;

        player.OnDetected += PlayerDetected;
        player.OnCatched += PlayerLose;
    }

    private void PlayerDetected()
    {
        // Trigger directly new turn.
        player.StartNewTurn();
    }

    public void PlayerLose()
    {
        if(IsGameOver)
        {
            return;
        }
        
        IsGameOver = true;

        if(OnLose != null)
        {
            OnLose();
        }
    }

    public void PlayerWin()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;

        if (OnWin != null)
        {
            OnWin();
        }
    }
}
