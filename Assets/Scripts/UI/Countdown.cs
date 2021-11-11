using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : SceneControl
{
    [SerializeField] private Text _Text;
    [SerializeField] private float _CountdownToStart = 3;

    private void Update()
    {
        if (_CountdownToStart > 1)
        {
            _CountdownToStart -= Time.deltaTime;
            DisplayCountdown();
        }
        else WestonSceneStart(); 
    }

    private void DisplayCountdown()
    {
        _Text.text = "Start in... " + _CountdownToStart.ToString("F0");
    }

}