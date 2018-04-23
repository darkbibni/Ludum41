using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour {

    [SerializeField] private Guard guard;

    [SerializeField] private SpriteRenderer[] sprites;
    
    [SerializeField] private LayerMask visionMask;

    private Vector3 guardOrigin;
    private Collider playerCollider;
    private int firstDistance;
    
    private void FixedUpdate()
    {
        guardOrigin = transform.position;
        guardOrigin += transform.right * 0.4f;

        playerCollider = null;

        firstDistance = 3;
        firstDistance = HandleRay(0);

        for (int z = -1; z <= 1; z+=2)
        {
            if(firstDistance > 0)
            {
                HandleRay(z);
            }

            else
            {
                UpdateLineVision(z, 0);
            }
        }

        DetectPlayer(playerCollider);
    }

    /// <summary>
    /// handle the ray of the specified vision line.
    /// </summary>
    /// <param name="z">Return the distance</param>
    /// <returns></returns>
    private int HandleRay(int z)
    {
        Vector3 rayOrigin;
        RaycastHit hit;

        // Place correclty ray origin.
        rayOrigin = guardOrigin;
        rayOrigin -= transform.forward * z;

        Ray r = new Ray(rayOrigin, transform.right);
        Debug.DrawRay(r.origin, r.direction * 3f, Color.green);

        int distance = firstDistance; // Vision range max.
        if (Physics.Raycast(r, out hit, 3f, visionMask))
        {
            distance = Mathf.RoundToInt(hit.distance);
            Debug.DrawRay(hit.point, r.direction * (3f - hit.distance), Color.red);

            if (hit.collider.tag == "Player")
            {
                playerCollider = hit.collider;
            }
        }

        UpdateLineVision(z, distance);

        return distance;
    }

    private void UpdateLineVision(int z, int distance)
    {
        // Update vision sprites depending the distance.
        int min = (z + 1) * 3;
        for (int i = 0; i < 3; i++)
        {
            if (i < distance)
            {
                sprites[min + i].gameObject.SetActive(true);
            }

            else
            {
                sprites[min + i].gameObject.SetActive(false);
            }
        }
    }
    
    private void DetectPlayer(Collider playerCollider)
    {
        if (playerCollider != null)
        {
            PlayerManager player = playerCollider.GetComponent<PlayerManager>();

            if (player)
            {
                player.DetectPlayer();
            }
        }
    }
}
