using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : SceneControl
{
    [Tooltip("Text field that text will be displayed in")] 
    [SerializeField] private Text _Text;

    [Tooltip("Text string displayed before the countdown timer")] 
    [SerializeField] private string _LeadingText;

    [Tooltip("Duration of countdown timer in seconds")] 
    [SerializeField] private float _CountdownToStart = 3;

    [Tooltip("Scene to transition to at end of countdown. Exits game if no scene entered.")] 
    [SerializeField] private Object _CountdownCompletionScene;
    
    [Tooltip("Bool indicator of countdown visibility")] 
    [SerializeField] private bool _IsCountdownVisible = true;


    private void Update()
    {
        if (_CountdownToStart > 1)
        {
            _CountdownToStart -= Time.deltaTime;
            if (_IsCountdownVisible) DisplayCountdown();
        }
        else if (_CountdownCompletionScene)
            SceneManager.LoadScene(_CountdownCompletionScene.name);
        else ExitGame();
    }

    private void DisplayCountdown()
    {
        _Text.text = _LeadingText + _CountdownToStart.ToString("F0");
    }

}