using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObstacleGroup
{
    public List<Obstacle> group;
}

public abstract class Obstacle : MonoBehaviour {
    
    public abstract void StartNewTurn();
}
