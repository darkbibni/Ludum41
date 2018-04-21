using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    [SerializeField] private Interactable interactable;

    private MissionManager missionMgr;

    [SerializeField] private bool permitToFlee = true;

    private bool isBreak;

    private void Awake()
    {
        isBreak = false;
        missionMgr = GameObject.FindObjectOfType<MissionManager>();

        interactable.OnInteract += Break;
    }

    private void Break()
    {
        if (isBreak)
        {
            return;
        }

        isBreak = true;

        // TODO Anim !
        // TODO Sound

        print("BREAK GLASS !");

        if(permitToFlee)
        {
            missionMgr.PlayerWin();
        }
    }
}
