using System.Collections;
using UnityEngine;

public class Guard : Obstacle {
    
    [SerializeField] private Patrol patrol;
    [SerializeField] private int baseMovePoint = 3;
    [SerializeField] private int bonusPointWhenAlerted = 6;

    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim;

    public bool IsAlerted
    {
        get
        {
            return isAlerted;
        }

        set
        {
            isAlerted = value;
        }
    }
    private bool isAlerted;
    
    private void Awake()
    {
        patrol.OnMoveSucceed += MoveStep;
    }

    public override void StartNewTurn()
    {
        StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        HasFinished = false;

        int movePoint = baseMovePoint;
        if(IsAlerted)
        {
            movePoint += bonusPointWhenAlerted;
        }

        yield return patrol.Move(movePoint);

        HasFinished = true;
    }

    private void MoveStep()
    {
        anim.SetTrigger("Move");

        audioSource.clip = AudioManager.instance.GetSound("SFX_FOOTSTEPS");
        audioSource.Play();
    }

    public void UseAlertPath()
    {
        // TODO Feedback
        patrol.UseAlertPath(isAlerted);
    }

    public void OnSeePlayer()
    {
        audioSource.clip = AudioManager.instance.GetSound("SFX_GUARD_ALERTED");
        audioSource.Play();

        // TODO FX !
    }
}
