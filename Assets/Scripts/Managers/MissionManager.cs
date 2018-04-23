using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

    [SerializeField] PlayerManager player;
    [SerializeField] TurnManager turnMgr;

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

    private void Update()
    {
        if(IsGameOver)
        {
            if(Input.GetButtonDown("Submit"))
            {
                Restart();
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayerDetected()
    {
        // Trigger directly new turn for the player (but it's the last one !)
        turnMgr.StartNewPlayerTurn();
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
