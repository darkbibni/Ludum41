using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

public class Door : Trigerrable {

    [SerializeField] private Collider[] colliders;

    //[SerializeField] private NavMeshObstacle navMeshObstacle;
    [SerializeField] private float OpenDuration = 1.0f;

    [SerializeField] private AudioSource audioSource;

    public override void Activate()
    {
        OpenDoor();

        audioSource.clip = AudioManager.instance.GetSound("SFX_OPEN_DOOR");
        audioSource.Play();
    }

    [ContextMenu("OPEN DOOR")]
    private void OpenDoor()
    {
        foreach (Collider c in colliders)
        {
            c.transform.DOLocalMove(c.transform.localPosition + c.transform.forward, OpenDuration);
        }
        //navMeshObstacle.enabled = false;
    }
}
