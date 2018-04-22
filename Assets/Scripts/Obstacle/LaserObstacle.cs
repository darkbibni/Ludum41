using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public enum LaserType
{
    QUARTER,
    DEMI,
    THIRD_QUARTER,
    FULL
}

public class LaserObstacle : Obstacle
{
    [SerializeField] private Transform laserTransform;
    [SerializeField] private LaserType laserType;
    [SerializeField] private float animDuration = 0.25f;

    private int currentStep;
    private bool horaireSens;

    private void Awake()
    {
        currentStep = 0;
        horaireSens = true;
    }

    public override void StartNewTurn()
    {
        DetermineNextStep();

        laserTransform.DOLocalRotate(new Vector3(0, 45f * currentStep, 0f), animDuration).SetEase(Ease.Linear);
    }

    private void DetermineNextStep()
    {
        int nextStep = currentStep;

        if (horaireSens)
        {
            nextStep++;

            if (nextStep == ((int)(laserType) + 1) * 2)
            {
                // Loop.
                if (laserType == LaserType.FULL)
                {
                    nextStep = 0;
                }

                // Round trip.
                else
                {
                    horaireSens = false;
                }
            }
        }

        else
        {
            nextStep--;

            if (nextStep == 0)
            {
                horaireSens = true;
            }
        }

        currentStep = nextStep;
    }
}
