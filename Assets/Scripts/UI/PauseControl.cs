using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : CharacterComponent
{
    public static bool gameIsPaused;
    private GameObject _PauseScreen;
  
    protected override void Start()
    {
        base.Start();
        // _PauseScreen = GameObject.Find("PauseCanvas") ;
        _PauseScreen = GameObject.Find("PauseCanvas") ;
        _PauseScreen.SetActive(false);
        Debug.Log(_PauseScreen.name);
    }

    protected override bool HandlePlayerInput()
    {
        if(!base.HandlePlayerInput()) return false;   
        if (PauseInput())
        {
            PauseGame();
        }

        return true;
    }

    private bool PauseInput()
    {
        if(Input.GetKeyDown(CharacterInputs.PauseKeyCode)) return true;
        
        return false;
    }

    void PauseGame ()
    {
        gameIsPaused = !gameIsPaused;
        if(gameIsPaused)
        {
            _PauseScreen.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
            Debug.Log("game paused");
            
        }
        else 
        {
            _PauseScreen.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}