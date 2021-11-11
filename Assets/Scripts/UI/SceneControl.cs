using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void MainMenu ()
    {
        Debug.Log("TRIED TO OPEN MAIN MENU SCENE");
        SceneManager.LoadScene("StartMenu");
    }
    
    public void StartGame ()
    {
        Debug.Log("TRIED TO START INTRO SCENE");
        SceneManager.LoadScene("Intro");
    }

    public void ControlsMenu ()
    {
        Debug.Log("TRIED TO OPEN CONTROLS MENU SCENE");
        SceneManager.LoadScene("ControlsMenu");
    }    

    protected void WestonSceneStart ()
    {
        Debug.Log("TRIED TO LOAD WESTON SCENE");
        SceneManager.LoadScene("WestonScene");
    }

    public void ExitScene ()
    {
        Debug.Log("TRIED TO LOAD EXIT SCENE");
        SceneManager.LoadScene("Exit");
        Invoke("ExitGame", 3);
    }

    public void ExitGame ()
    {
        Debug.Log("TRIED TO EXIT GAME THROUGH QUIT FUNCTION");
        SceneManager.LoadScene("EndOfGame");
        Application.Quit();
    }
}