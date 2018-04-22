using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    [SerializeField] private Interactable interactable;
    
    [SerializeField] private AudioSource audioSource;

    private MissionManager missionMgr;

    [SerializeField] private bool permitToFlee = true;

    private bool isBreak;

    private void Awake()
    {
        isBreak = false;
        missionMgr = GameObject.FindObjectOfType<MissionManager>();

        interactable.OnInteract += Break;
    }

    [ContextMenu("Test Son")]
    private void Break()
    {
        if (isBreak)
        {
            return;
        }

        isBreak = true;

        // TODO Feedback - Anim !

        audioSource.clip = AudioManager.instance.GetSound("SFX_BREAK_GLASS");
        audioSource.Play();

        if(permitToFlee)
        {
            missionMgr.PlayerWin();
        }
    }
}
