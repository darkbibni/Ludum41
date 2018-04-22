using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Obstacle {
    
    [SerializeField] private Patrol patrol;
    [SerializeField] private int movePoint = 6;

    public override void StartNewTurn()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        HasFinished = false;

        yield return patrol.Move(movePoint);

        HasFinished = true;
    }
}
