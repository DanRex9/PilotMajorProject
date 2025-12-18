using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        //script for play button
        SceneManager.LoadSceneAsync(1); 
    }

    public void Quitgame()
    {
        //Script to quit game
        Application.Quit();
    }
}
