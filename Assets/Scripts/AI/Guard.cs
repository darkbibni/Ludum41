using System.Collections;
using UnityEngine;

public class Guard : Obstacle {
    
    [SerializeField] private Patrol patrol;
    [SerializeField] private int baseMovePoint = 3;
    [SerializeField] private int bonusPointWhenAlerted = 6;

    public bool IsAlerted
    {
        get
        {
            return isAlerted;
        }

        set
        {
            isAlerted = true;

            patrol.UseAlertPath(isAlerted);
            // TODO FEEDBACK !!!
        }
    }
    private bool isAlerted;

    public override void StartNewTurn()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        HasFinished = false;

        int movePoint = baseMovePoint;
        if(IsAlerted)
        {
            movePoint += bonusPointWhenAlerted;
        }

        yield return patrol.Move(movePoint);

        HasFinished = true;
    }
}
