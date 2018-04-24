﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    [SerializeField] private Interactable interactable;
    
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private PlayerManager player;
   
    [SerializeField] private bool permitToFlee = true;

    private bool isBreak;

    private void Awake()
    {
        isBreak = false;

        interactable.OnInteract += Break;
    }
    
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
            player.Flee();
        }
    }
}
