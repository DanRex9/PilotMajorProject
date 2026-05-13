using UnityEngine;
using System.Collections;

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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            levelLoaded = true;
            AudioManager.Instance.PlayBackgroundMusic();
            AudioManager.Instance.PlayMusic();
        }
    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Level Loaded");
        AudioManager.Instance.PlayBackgroundMusic();
        AudioManager.Instance.PlayMusic();
    }
}
