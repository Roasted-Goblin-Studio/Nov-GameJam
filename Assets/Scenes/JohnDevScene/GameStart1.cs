using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart1 : MonoBehaviour
{
    public void StartGame ()
    {
        Debug.Log("TRIED TO START LEVEL 1");
        SceneManager.LoadScene("JohnDevScene");
    }

    public void Controls ()
    {
        Debug.Log("TRIED TO OPEN CONTROLS SCREEN");
        SceneManager.LoadScene("Controls");
    }    

    public void ExitGame ()
    {
        Debug.Log("TRIED TO EXIT GAME");
        Application.Quit();
    }    
}