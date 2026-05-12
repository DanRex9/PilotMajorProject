using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToStart : MonoBehaviour
{
    private bool KeyPressed = false;

    private void Update()
    {
        if (!KeyPressed && Input.anyKeyDown)
        {
            KeyPressed = true;
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }
}
