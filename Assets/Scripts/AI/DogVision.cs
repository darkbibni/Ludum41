using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogVision : MonoBehaviour {

    [SerializeField] private Dog dog;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            dog.WakeUp();
        }
    }
}
