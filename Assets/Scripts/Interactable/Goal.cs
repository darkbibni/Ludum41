using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    [SerializeField] private Interactable lootable;

    private void Awake()
    {
        lootable.OnInteract += GoalInteraction;
    }

    private void GoalInteraction()
    {
        // TODO Display description

        // Destroy when close description.
    }
}
