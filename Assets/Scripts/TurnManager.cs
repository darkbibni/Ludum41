using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    [SerializeField] private MissionManager missionMgr;

    [SerializeField] private PlayerManager player;
    [SerializeField] private List<Obstacle> missionObstacles;

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
        // Check first if an exit has been reached.

        // Player hasn't succeed to flee.
        if(player.HasBeenDetected)
        {
            player.CatchPlayer();

            return;
        }

        isPlayerTurn = false;

        // TODO Trigger all computer elements.
        foreach(Obstacle obstacle in missionObstacles)
        {
            obstacle.OnNewTurn();
        }

        StartCoroutine(FakeComputerTurn());
    }

    void StartNewPlayerTurn()
    {
        isPlayerTurn = true;

        player.StartNewTurn();
    }

    IEnumerator FakeComputerTurn()
    {
        yield return new WaitForSeconds(0.5f);

        StartNewPlayerTurn();
    }
}
