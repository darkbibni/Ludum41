using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField] private Interactable lootable;

    [SerializeField] private List<Trigerrable> triggerables;

    [SerializeField] private Animator anim;

    private bool hasBeenUsed;

    private void Awake()
    {
        lootable.OnInteract += InteractWithButton;

        hasBeenUsed = false;
    }

    private void InteractWithButton()
    {
        if(hasBeenUsed)
        {
            return;
        }

        anim.SetTrigger("Use");

        hasBeenUsed = true;

        foreach(Trigerrable triggerable in triggerables)
        {
            triggerable.Activate();
        }
    }
}
