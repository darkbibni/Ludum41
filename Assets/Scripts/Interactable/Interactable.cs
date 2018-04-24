using System;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Orientation Orientation
    {
        get { return playerOrientation; }
    }
    [SerializeField] private Orientation playerOrientation;
    [SerializeField] private Orientation discreteOrientation = Orientation.None;

    public bool HasDiscretInteraction
    {
        get { return hasDiscreteInteraction; }
    }
    [SerializeField] private bool hasDiscreteInteraction;

    public Delegates.SimpleDelegate OnInteract;
    public Delegates.SimpleDelegate OnDiscreteInteract;

    public virtual void Interact(Vector3 dir)
    {
        if (playerOrientation != Orientation.None && dir != Vector3.zero)
        {
            // Check the player orientation when interact.
            if(playerOrientation != OrientationUtility.GetOrientation(dir)) {
                return;
            }
        }

        if (OnInteract != null)
        {
            OnInteract();
        }
    }

    public virtual void DiscreteInteract(Vector3 dir)
    {
        if (discreteOrientation != Orientation.None && dir != Vector3.zero)
        {
            // Check the player orientation when interact.
            if (discreteOrientation != OrientationUtility.GetOrientation(dir))
            {
                return;
            }
        }

        if (OnDiscreteInteract != null)
        {
            OnDiscreteInteract();
        }
    }

    public virtual void OnHoverEnd()
    {
        
    }

    public virtual void OnHoverStart()
    {

    }
}
