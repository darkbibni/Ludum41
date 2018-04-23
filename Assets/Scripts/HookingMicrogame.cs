using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookingMicrogame : MonoBehaviour {

    [SerializeField] private Image player;
    [SerializeField] private Image area;

    public LockPickingPreset Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }
    private LockPickingPreset difficulty;

    // Min and max of the micro game.
    private float min = 0f, max = 180f;

    private bool sensHoraire = false;

    private float t;
    private Vector2 areaLimit;

    private Quaternion start, end;

    public bool IsFinished { get; private set; }
    public bool HasSucceed { get; private set; }

    private void Awake()
    {
        start = Quaternion.Euler(0, 0, min);
        end = Quaternion.Euler(0, 0, max + 360f*player.fillAmount);

        t = 0f;
        sensHoraire = true;

        area.fillAmount = difficulty.size * 0.5f;

        float targetT = Random.Range(0f, 1f - difficulty.size);
        
        area.transform.localRotation = Quaternion.Lerp(start, end, targetT);
        areaLimit.x = targetT;
        areaLimit.y = targetT + difficulty.size * 0.8f;
    }

    public void StartMg()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!enabled)
        {
            return;
        }

        UpdatePlayerPos();

        player.transform.localRotation = Quaternion.Lerp(start, end, t);

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("DiscreteAction"))
        {
            if (CheckPlayerPos())
            {
                HasSucceed = true;
            }

            else
            {
                HasSucceed = false;
            }

            IsFinished = true;

            gameObject.SetActive(false);
        }
    }

    // Check if the cursor is correclty placed.
    private bool CheckPlayerPos()
    {
        if(t >= areaLimit.x && t <= areaLimit.y)
        {
            return true;
        }

        return false;
    }

    // Move the cursor.
    private void UpdatePlayerPos()
    {
        if (sensHoraire)
        {
            t += Time.deltaTime * difficulty.speed;

            if (t >= 1f)
            {
                sensHoraire = false;
            }
        }

        else
        {
            t -= Time.deltaTime * difficulty.speed;

            if (t <= 0f)
            {
                sensHoraire = true;
            }
        }
    }
}
