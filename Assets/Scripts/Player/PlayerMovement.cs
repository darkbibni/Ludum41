using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Rigidbody rgbd;
    [SerializeField] private PlayerInput input;

    [SerializeField] private LayerMask wallMask;
    [SerializeField] private Transform spriteRot;

    public Delegates.SimpleDelegate OnMoveSucceed;

    private void Awake()
    {
        input.move += Move;
    }

    void Move(float moveH, float moveV)
    {
        Vector3 dir = (Vector3.right * moveH + Vector3.forward * moveV).normalized;
        Ray ray = new Ray(transform.position, dir.normalized);
        
        // Check if a wall exist in front of the player.
        if (!Physics.Raycast(ray, 1f, wallMask))
        {
            rgbd.MovePosition(transform.position + dir);

            spriteRot.localRotation = Quaternion.LookRotation(dir);

            if (OnMoveSucceed != null)
            {
                OnMoveSucceed();
            }
        }
    }
}
