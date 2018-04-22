using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDebug : MonoBehaviour {
    [SerializeField] private AudioSource TestSon;

    [ContextMenu("Test Son")]
    public void TestSound()
    {
        TestSon.Play();
    }

    // Use this for initialization
    void Start() {




    }
    }