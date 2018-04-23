using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObstacleGroup
{
    public List<Obstacle> group;
}

public abstract class Obstacle : MonoBehaviour {

    public bool HasFinished { get; protected set; }

    public abstract void StartNewTurn();
}
