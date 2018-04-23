using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Obstacle
{
    [SerializeField] private List<Guard> guards;
    
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    private Transform player;
    private bool isWakeUp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void StartNewTurn()
    {
        if(isWakeUp)
        {
            // TODO GROGNER !!!
            audioSource.clip = AudioManager.instance.GetSound("SFX_DOG_GRUNT");

            // Look at player !!!

            transform.LookAt(player);
        }

        else
        {
            audioSource.clip = AudioManager.instance.GetSound("SFX_DOG_SLEEPING");
            // TODO zZzZ animation or continuously ?
        }

        audioSource.Play();
    }

    public void WakeUp()
    {
        anim.SetTrigger("WakeUp");

        audioSource.clip = AudioManager.instance.GetSound("SFX_DOG_WOOF");
        audioSource.Play();

        AlertGuard();
    }

    public void AlertGuard()
    {
        foreach (Guard guard in guards)
        {
            guard.IsAlerted = true;
        }
    }
}
