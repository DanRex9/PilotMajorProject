using UnityEngine;
using UnityEngine.SceneManagement;

public enum Sounds
    {
        coinpickup, 
        coindrop, 
        pirateyay1, 
        pirateyay2, 
        piratenay1, 
        pioratenay2, 
        winpirate, 
        loosepirate, 
        buttonsound, 
        background, 
        background2   
    }


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set;}
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource backgroundSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip background2;
    public AudioClip coinpickup;
    public AudioClip coindrop;
    public AudioClip pirateyay1;
    public AudioClip pirateyay2;
    public AudioClip piratenay1;
    public AudioClip pioratenay2;
    public AudioClip winpirate;
    public AudioClip loosepirate;
    public AudioClip buttonsound;

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "PilotGame")
        {
            musicSource.clip = background;
            musicSource.Play();

            backgroundSource.clip = background2;
            backgroundSource.Play();
        }
        
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.coinpickup:
                PlaySFX(coinpickup, 0.1f);
                break;
            case Sounds.coindrop:
                PlaySFX(coindrop, 0.2f);
                break;
            case Sounds.pirateyay1:
                PlaySFX(pirateyay1, 0.1f);
                break;
            case Sounds.pirateyay2:
                PlaySFX(pirateyay2, 0.1f);
                break;
            case Sounds.piratenay1:
                PlaySFX(piratenay1, 0.1f);
                break;
            case Sounds.pioratenay2:
                PlaySFX(pioratenay2, 0.1f);
                break;
            case Sounds.winpirate:
                PlaySFX(winpirate, 0.1f);
                break;
            case Sounds.loosepirate:
                PlaySFX(loosepirate, 0.1f);
                break;
            case Sounds.buttonsound:
                PlaySFX(buttonsound, 0.2f);
                break;
        }
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip, volume);
        Debug.Log($"Playing sound volume: {SFXSource.volume} - Clip:  {clip.name}");
    }

    public void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayBackgroundMusic()
    {
        backgroundSource.clip = background2;
        backgroundSource.Play();
    }

    public void PlayMusic()
    {
        backgroundSource.clip = background;
        musicSource.Play();
    }
}

