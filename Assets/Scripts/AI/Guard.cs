using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Obstacle {

    private bool hasDetectPlayer;

    [SerializeField] private Patrol patrol;
    [SerializeField] private int movePoint = 6;

    public override void StartNewTurn()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return patrol.Move(movePoint);
    }

    // TODO Check if detect player
}
