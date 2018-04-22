﻿using System;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] private Orientation playerOrientation;

    public bool HasDiscretInteraction
    {
        get { return hasDiscreteInteraction; }
    }
    [SerializeField] private bool hasDiscreteInteraction;

    public Delegates.SimpleDelegate OnInteract;
    public Delegates.SimpleDelegate OnDiscreteInteract;

    public virtual void Interact(Vector3 dir)
    {
        print("INTERACT with dir : " + dir);

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
        print("INTERACT DISCRETLY with dir : " + dir);

        if (playerOrientation != Orientation.None && dir != Vector3.zero)
        {
            // Check the player orientation when interact.
            if (playerOrientation != OrientationUtility.GetOrientation(dir))
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
