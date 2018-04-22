using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookingMicrogame : MonoBehaviour {

    [SerializeField] private Image player;
    [SerializeField] private Image area;
    [SerializeField] private float microGameSpeed = 0.5f;

    [Range(0f, 1f)]
    [SerializeField] private float microGameSize = 0.1f;

    [SerializeField] private float min = 0f;
    [SerializeField] private float max = 180f;

    private bool sensHoraire = false;

    private float t;
    private Vector2 areaLimit;

    private Quaternion start;
    private Quaternion end;

    public bool IsFinished;
    public bool HasSucceed;

    private void Awake()
    {
        start = Quaternion.Euler(0, 0, min);
        end = Quaternion.Euler(0, 0, max + 360f*player.fillAmount);

        t = 0f;
        sensHoraire = true;

        area.fillAmount = microGameSize * 0.5f;

        float targetT = Random.Range(0f, 1f - microGameSize);
        
        area.transform.localRotation = Quaternion.Lerp(start, end, targetT);
        areaLimit.x = targetT;
        areaLimit.y = targetT + microGameSize * 0.8f;
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

        if (Input.GetButtonDown("Submit"))
        {
            if(CheckPlayerPos())
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

    private bool CheckPlayerPos()
    {
        if(t >= areaLimit.x && t <= areaLimit.y)
        {
            return true;
        }

        return false;
    }

    private void UpdatePlayerPos()
    {
        if (sensHoraire)
        {
            t += Time.deltaTime * microGameSpeed;

            if (t >= 1f)
            {
                sensHoraire = false;
            }
        }

        else
        {
            t -= Time.deltaTime * microGameSpeed;

            if (t <= 0f)
            {
                sensHoraire = true;
            }
        }
    }
}
