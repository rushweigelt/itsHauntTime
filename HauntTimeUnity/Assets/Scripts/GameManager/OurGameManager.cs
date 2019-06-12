using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OurGameManager : Singleton<OurGameManager>
{
    public bool paused;

    public float finalHUDDelay;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {

    }

    /// <summary>
    /// Freezes/unfreezes game
    /// </summary>
    /// <param name="active">Unfreezes game if true, freezes if false</param>
    private void SetGameActive(bool active)
    {
        // Pause game
        paused = !active;
        Time.timeScale = active ? 1f : 0f;
    }

    /// <summary>
    /// Pause game and update UI accordingly
    /// </summary>
    public void Pause()
    {
        if(!paused)
        {
            // Call HUD events
            HUDManager.Instance.onPaused.Invoke();

            SetGameActive(false);
        }
    }

    /// <summary>
    /// Unpause game and update UI accordingly
    /// </summary>
    public void unPause()
    {
        if(paused)
        {
            // Call HUD events
            HUDManager.Instance.onUnpaused.Invoke();

            // Unpause game
            SetGameActive(true);
        }
    }

    public void GameOver(bool playerWon)
    {
        /*
        // Call HUD events
        HUDManager.Instance.onGameOver.Invoke();
        SetGameActive(false);
        */
        StartCoroutine(DelayFinalHUD());
        
        // TODO: handle game over condition
        if (playerWon)
        {
            // set "you win!" text

        }
        else
        {
            // set "you lose..." text

        }
    }
    IEnumerator DelayFinalHUD()
    {
        yield return new WaitForSeconds(finalHUDDelay);
        HUDManager.Instance.onGameOver.Invoke();
        SetGameActive(false);
    }
}
