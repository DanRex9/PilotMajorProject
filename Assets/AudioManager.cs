using UnityEngine;

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
        musicSource.clip = background;
        musicSource.Play();

        backgroundSource.clip = background2;
        backgroundSource.Play();
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.coinpickup:
                PlaySFX(coinpickup, 0.5f);
                break;
            case Sounds.coindrop:
                PlaySFX(coindrop, 0.5f);
                break;
            case Sounds.pirateyay1:
                PlaySFX(pirateyay1, 1f);
                break;
            case Sounds.pirateyay2:
                PlaySFX(pirateyay2, 1f);
                break;
            case Sounds.piratenay1:
                PlaySFX(piratenay1, 1f);
                break;
            case Sounds.pioratenay2:
                PlaySFX(pioratenay2, 1f);
                break;
            case Sounds.winpirate:
                PlaySFX(winpirate, 1f);
                break;
            case Sounds.loosepirate:
                PlaySFX(loosepirate, 1f);
                break;
            case Sounds.buttonsound:
                PlaySFX(buttonsound, 1f);
                break;
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}

