using System.Collections.Generic;
using UnityEngine;

public enum PatrolType
{
    // Flip and going back when reach an extremity of the patrol path.
    GOING_AND_COMING,

    // Go to the first patrol point when it reach the end.
    CYCLIC,

    // Teleport to the first patrol point when it reach the end.
    WARP
}

public class Patrol : MonoBehaviour
{
    #region Inspector attributes

    public float patrolSpeed = 1.0f;
    public PatrolType patrolType;
    
    public List<Transform> patrolPoints = new List<Transform>();

    #endregion

    #region Private attributes
    
    protected Vector3 initialPosition;
    protected Vector3 nextPatrolPoint;
    private int nextPatrolPointIndex;

    private bool reverseDirection = false;

    #endregion

    #region Unity methods

    // Use this for initialization
    protected void Awake()
    {
        initialPosition = transform.position;

        SetupAI();
    }

    // Update is called once per frame
    protected void Update()
    {
        float distance = Vector3.Distance(transform.position, nextPatrolPoint);

        if (distance > Mathf.Epsilon)
            transform.position = Vector3.MoveTowards(transform.position, nextPatrolPoint, patrolSpeed * Time.deltaTime);

        else
            DetermineNextPatrolPoint();
    }

    #endregion

    #region Patrol methods

    // Determine the next patrol point to reach depending the type of patrol choosen.
    protected void DetermineNextPatrolPoint()
    {
        switch (patrolType)
        {
            case PatrolType.GOING_AND_COMING:

                GoingAndComingPatrol(); break;

            case PatrolType.CYCLIC:

                CyclicPatrol(); break;

            case PatrolType.WARP:

                WarpPatrol(); break;
        }
    }

    protected void GoingAndComingPatrol()
    {
        if (nextPatrolPointIndex == patrolPoints.Count)
        {
            reverseDirection = true;
            nextPatrolPointIndex = patrolPoints.Count - 1;
        }
        else if (nextPatrolPointIndex == -1)
        {
            reverseDirection = false;
            nextPatrolPointIndex = 1;
        }

        if (reverseDirection)
            nextPatrolPoint = patrolPoints[nextPatrolPointIndex--].position;
        else
            nextPatrolPoint = patrolPoints[nextPatrolPointIndex++].position;
    }

    protected void CyclicPatrol()
    {
        if (nextPatrolPointIndex == patrolPoints.Count)
        {
            nextPatrolPointIndex = 0;
        }

        nextPatrolPoint = patrolPoints[nextPatrolPointIndex++].position;
    }

    protected void WarpPatrol()
    {
        if (nextPatrolPointIndex == patrolPoints.Count)
        {
            transform.position = patrolPoints[0].position;
            nextPatrolPointIndex = 1;
        }

        nextPatrolPoint = patrolPoints[nextPatrolPointIndex++].position;
    }

    #endregion

    public void SetupAI()
    {
        transform.position = initialPosition;
        nextPatrolPointIndex = 1;

        DetermineNextPatrolPoint();
    }
}