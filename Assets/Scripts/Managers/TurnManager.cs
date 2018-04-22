using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    
    [SerializeField] private PlayerManager player;
    [SerializeField] private List<Obstacle> missionObstacles;

    public Delegates.SimpleDelegate OnPlayerTurn;
    public Delegates.SimpleDelegate OnComputerTurn;

    public bool IsPlayerTurn
    {
        get
        {
            return isPlayerTurn;
        }
    }
    private bool isPlayerTurn;

    private void Awake()
    {
        player.OnTurnFinished += OnPlayerTurnFinished;

        StartNewPlayerTurn();
    }

    void OnPlayerTurnFinished()
    {
        // Player hasn't succeed to flee.
        if(player.HasBeenDetected)
        {
            player.CatchPlayer();

            return;
        }

        isPlayerTurn = false;
        if(OnComputerTurn != null)
        {
            OnComputerTurn();
        }
        
        // Trigger all museum elements.
        foreach(Obstacle obstacle in missionObstacles)
        {
            obstacle.StartNewTurn();
        }

        StartCoroutine(FakeComputerTurn());
    }

    void StartNewPlayerTurn()
    {
        isPlayerTurn = true;
        
        player.StartNewTurn();
        
        if (OnPlayerTurn != null)
        {
            OnPlayerTurn();
        }
    }

    IEnumerator FakeComputerTurn()
    {
        yield return new WaitForSeconds(0.5f);

        StartNewPlayerTurn();
    }
}
