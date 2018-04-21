using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Door : Trigerrable {

    [SerializeField] private Collider[] colliders;

    public override void Activate()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        foreach (Collider c in colliders)
        {
            c.transform.DOLocalMove(c.transform.localPosition + c.transform.forward, 0.5f);
        }
    }
}
