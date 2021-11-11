using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour
{

    // Pausing
    [SerializeField] private KeyCode _PauseKeyCode = KeyCode.F;
    public KeyCode PauseKeyCode {get => _PauseKeyCode; set => _PauseKeyCode = value;}
    [SerializeField] private Text text;

    // Pause state
    [SerializeField] private static bool _GameIsPaused;
    public static bool GameIsPaused { get => _GameIsPaused; set => _GameIsPaused = value; }
    private GameObject _PauseScreen;

    private Character _Character;

    public string[] PauseStrings = {
        "this is a test",
        "whatever",
        "continue",
        "something else"
    };

    protected void Start()
    {
        // base.Start();
        _PauseScreen = GameObject.Find("PauseCanvas");
        _PauseScreen.SetActive(false);
        Debug.Log(_PauseScreen.name);
        Debug.Log("start working");
    }

    protected void Update()
    {
        if(Input.GetKeyDown(PauseKeyCode))
        {
            PauseGame();
            Debug.Log("pause pressed");
        }
    }

    protected void PauseGame ()
    {
        GameIsPaused = !GameIsPaused;
        if(GameIsPaused)
        {
            _PauseScreen.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
            text.text = PauseStrings[Random.Range(0, PauseStrings.Length)];
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