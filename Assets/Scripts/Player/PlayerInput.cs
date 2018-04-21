using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [SerializeField] TurnManager turnMgr;
    [SerializeField] MissionManager missionMgr;

    public delegate void Move(float horizontal, float vertical);
    public Move move;
    public Delegates.SimpleDelegate OnAction;

    private float horizontal, vertical;

	// Update is called once per frame
	void Update ()
    {
        if(!turnMgr.IsPlayerTurn || missionMgr.IsGameOver)
        {
            return;
        }

        HandleMovementInput();

        HandleAction();
    }

    private void HandleMovementInput()
    {
        // Handle movement input.
        horizontal = 0f;
        vertical = 0f;

        if (Input.GetButtonDown("Horizontal"))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        else if (Input.GetButtonDown("Vertical"))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }

        if (horizontal != 0f || vertical != 0f)
        {
            if (move != null)
            {
                move(horizontal, vertical);
            }
        }
    }

    private void HandleAction()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (OnAction != null)
            {
                OnAction();
            }
        }
    }
}
