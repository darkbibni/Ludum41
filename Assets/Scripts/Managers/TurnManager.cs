using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    
    [SerializeField] private PlayerManager player;
    [SerializeField] private List<ObstacleGroup> missionObstacles;

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

    public void OnPlayerTurnFinished()
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

        StartCoroutine(WaitForComputer());
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

    /// <summary>
    /// Wait the computer turn.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForComputer()
    {
        // Trigger all museum elements.
        foreach (ObstacleGroup obstacleGroup in missionObstacles)
        {
            int groupCount = obstacleGroup.group.Count;

            for (int i = 0; i < groupCount; i++)
            {
                obstacleGroup.group[i].StartNewTurn();

                // Wait for the last element of the group.
                if (i == groupCount-1)
                {
                    yield return new WaitUntil(() => obstacleGroup.group[i].HasFinished);
                }
            }
        }

        StartNewPlayerTurn();
    }
}
