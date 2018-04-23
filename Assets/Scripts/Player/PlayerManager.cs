using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerInteraction interaction;

    [SerializeField] private int movePointBase = 5;
    [SerializeField] private int bonusPointLastTurn = 2;

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSrc;

    private int currentMovePoint;

    public int CurrentMovePoint
    {
        get { return currentMovePoint; }
    }

    public bool HasBeenDetected
    {
        get; private set;
    }
    public bool HasBeenCatched {
        get; private set;
    }

    public Delegates.SimpleDelegate OnMoveDone;
    public Delegates.SimpleDelegate OnTurnFinished;
    public Delegates.SimpleDelegate OnDetected;
    public Delegates.SimpleDelegate OnCatched;

    void Awake()
    {
        move.OnMoveSucceed += MoveSucceed;
        interaction.OnInteract += InteractionSucceed;
    }

    public void StartNewTurn()
    {
        currentMovePoint = movePointBase;

        if(HasBeenDetected)
        {
            currentMovePoint += bonusPointLastTurn;
        }
    }

    void MoveSucceed()
    {
        currentMovePoint--;

        if(OnMoveDone != null)
        {
            OnMoveDone();
        }

        anim.SetTrigger("Move");
        audioSrc.PlayOneShot(AudioManager.instance.GetSound("SFX_FOOTSTEPS"));

        if (currentMovePoint <= 0)
        {
            FinishPlayerTurn();
        }
    }

    void InteractionSucceed()
    {
        anim.SetTrigger("Action");
    }

    public void FinishPlayerTurn()
    {
        // TODO Some things...

        if(OnTurnFinished != null)
        {
            OnTurnFinished();
        }
    }
    
    public void DetectPlayer()
    {
        if(HasBeenDetected)
        {
            return;
        }

        HasBeenDetected = true;

        if (OnDetected != null)
        {
            OnDetected();
        }
    }

    public void CatchPlayer()
    {
        if(HasBeenCatched)
        {
            return;
        }

        HasBeenCatched = true;

        if (OnCatched != null)
        {
            OnCatched();
        }
    }
}
