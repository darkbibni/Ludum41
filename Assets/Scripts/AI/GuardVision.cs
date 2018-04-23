using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour {

    [SerializeField] private Guard guard;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if(player)
            {
                player.DetectPlayer();
            }
        }
    }
}
