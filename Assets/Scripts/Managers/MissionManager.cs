using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

    [SerializeField] PlayerManager player;
    [SerializeField] TurnManager turnMgr;
    [SerializeField] float restartCooldown = 1f;

    public Delegates.SimpleDelegate OnWin;
    public Delegates.SimpleDelegate OnLose;

    public bool IsGameOver
    {
        get; private set;
    }

    private bool canRestart;

    private void Awake()
    {
        IsGameOver = false;

        player.OnDetected += OnPlayerDetected;
        player.OnCatched += OnPlayerCatched;
    }
    
    private void Update()
    {
        if(IsGameOver && canRestart)
        {
            if(Input.GetButtonDown("Submit"))
            {
                Restart();
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPlayerDetected()
    {
        // Trigger directly new turn for the player (but it's the last one !)
        turnMgr.StartNewPlayerTurn();
    }

    public void OnPlayerCatched()
    {
        Lose();
    }

    public void Lose()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;

        if (OnLose != null)
        {
            OnLose();
        }
    }

    public void Win()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;

        if (OnWin != null)
        {
            OnWin();
        }

        StartCoroutine(PermitToRestart());
    }

    private IEnumerator PermitToRestart()
    {
        yield return new WaitForSeconds(restartCooldown);

        canRestart = true;
    }
}
