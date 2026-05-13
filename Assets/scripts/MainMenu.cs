using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    AudioManager audioManager;

    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }
    public void Playgame()
    {
        //script for play button
        //audioManager.PlaySound(AudioManager.Sounds.buttonsound);
        AudioManager.Instance.PlayBackgroundMusic();
        AudioManager.Instance.PlayMusic();
        SceneManager.LoadSceneAsync("PilotGame"); 
    }

    public void GoToMenu()
    {
        //script for play button
        //audioManager.PlaySound(AudioManager.Sounds.buttonsound);
        SceneManager.LoadSceneAsync("Main Menu"); 
    }

    public void PlayButtonSound()
    {
        audioManager.PlaySound(Sounds.buttonsound);
    }

    public void Quitgame()
    {
        //Script to quit game
        Application.Quit();
    }
}
