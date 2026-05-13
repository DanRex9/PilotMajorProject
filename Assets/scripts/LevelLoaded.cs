using UnityEngine;

public class LevelLoaded : MonoBehaviour
{
    bool levelLoaded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!levelLoaded)
        {
            levelLoaded = true;
            AudioManager.Instance.StopBackgroundMusic();
            AudioManager.Instance.StopMusic();
        }
    }
}
