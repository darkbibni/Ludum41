using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

    [SerializeField] private Guard guard;

    [SerializeField] private Collider[] colliders;
    [SerializeField] private SpriteRenderer[] sprites;

    [SerializeField] private LayerMask visionCutMask;

    private void FixedUpdate()
    {
        Ray r = new Ray(transform.position, transform.parent.forward);
        RaycastHit hit;

        int distance = 3;

        if(Physics.Raycast(r, out hit, 3f, visionCutMask)) {

            distance = Mathf.RoundToInt(hit.distance);
        }

        for (int i = 0; i < 3; i++)
        {
            if(i < distance)
            {
                colliders[i].enabled = true;
                sprites[i].enabled = true;
            }
            else
            {
                colliders[i].enabled = false;
                sprites[i].enabled = false;
            }
        }
    }

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
