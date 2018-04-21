using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMissionUI : MonoBehaviour {

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private MissionManager missionMgr;

    private void Awake()
    {
        missionMgr.OnWin += FeedbackWin;
        missionMgr.OnLose += FeedbackLose;
    }

    private void FeedbackWin()
    {
        winPanel.SetActive(true);
    }

    private void FeedbackLose()
    {
        losePanel.SetActive(true);
    }
}
