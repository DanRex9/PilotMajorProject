using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        //script for play button
        SceneManager.LoadSceneAsync("PilotGame"); 
    }

    public void GoToMenu()
    {
        //script for play button
        SceneManager.LoadSceneAsync("Main Menu"); 
    }

    public void Quitgame()
    {
        //Script to quit game
        Application.Quit();
    }
}
