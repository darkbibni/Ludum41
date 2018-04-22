using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField] private Interactable interactable;

    [SerializeField] private List<Trigerrable> triggerables;

    [SerializeField] private Animator anim;

    [SerializeField] private AudioSource audioSource;

    private bool hasBeenUsed;

    private void Awake()
    {
        interactable.OnInteract += InteractWithButton;

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

        audioSource.clip = AudioManager.instance.GetSound("SFX_PRESS_BUTTON");
        audioSource.Play();

        foreach (Trigerrable triggerable in triggerables)
        {
            triggerable.Activate();
        }
    }
}
