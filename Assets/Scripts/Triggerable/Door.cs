using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Trigerrable {

    [SerializeField] private Collider[] colliders;
    // TODO Disable collider.
    // TODO Anim

    public override void Activate()
    {
        foreach(Collider c in colliders)
        {
            c.gameObject.SetActive(false);
        }
    }
}
