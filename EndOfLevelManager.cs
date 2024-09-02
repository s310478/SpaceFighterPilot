using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public GameManager gameManager;

    public void StartEndOfLevelSequence()
    {
        playerControls.DisableControls();
        playerControls.StopFiring();
        playerControls.StartMovingToCenter();
    }

    public void OnTimelineEnd()
    {
        gameManager.ReloadLevel();
    }
}
