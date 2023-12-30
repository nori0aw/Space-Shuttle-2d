

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip bumpAudioClip;
    [SerializeField] private AudioClip gemAudioClip;
    [SerializeField] private SpaceShuttle spaceShuttle;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] GameObject audioClip2dObj;

    private AudioSource mainThrusterAudioSource;
    private AudioSource smallThrusterAudioSource;
    private float soundEffectsvolume;

    private int currentTrackNumber;

    private string SOUND_EFFECTS_VLOUME_KEY = "SOUND_EFFECTS_VLOUME_KEY";
    private string MUSIC_VLOUME_KEY = "MUSIC_VLOUME_KEY";
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;


        mainThrusterAudioSource = spaceShuttle.transform.GetComponents<AudioSource>()[0];
        smallThrusterAudioSource = spaceShuttle.transform.GetComponents<AudioSource>()[1];


        //I am trying to load all the audio clips at once to avoid game stutter when the audio clip is changing
        foreach(AudioClip clip in audioClips)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        musicAudioSource.Stop();
        /////////////////////////////////////


    }

    void Start()
    {

        soundEffectsvolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VLOUME_KEY, 0.65f);    
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VLOUME_KEY, this.GetMusicVolume()));
        currentTrackNumber = Random.Range(0, audioClips.Length);
        musicAudioSource.clip = audioClips[currentTrackNumber];
        musicAudioSource.Play();

    }

    private void Update()
    {
        if (!musicAudioSource.isPlaying)
        {
            currentTrackNumber = (currentTrackNumber + 1 ) % audioClips.Length;
            musicAudioSource.clip = audioClips[currentTrackNumber];
            musicAudioSource.Play();
        }
    }


    private void PlaySound(AudioClip audioClip, Vector3 position, float Multiplier = 1f)
    {
        GameObject AudioSource2d =  Instantiate(audioClip2dObj);
        AudioSource2d.transform.position = position;
        AudioSource2d.GetComponent<AudioSource>().volume = soundEffectsvolume * Multiplier;
        AudioSource2d.GetComponent<AudioSource>().clip = audioClip;
        AudioSource2d.GetComponent<AudioSource>().Play();
    }

    public void PlayBumpSound()
    {
        PlaySound(bumpAudioClip, spaceShuttle.transform.position, 0.4f);
    }

    public void PlayGemSound()
    {
        PlaySound(gemAudioClip, spaceShuttle.transform.position, 0.2f);
    }

    public void PlayBigThrusterSound(bool activate, float Multiplier = 0)
    {
        if (activate)
        {

            mainThrusterAudioSource.volume = soundEffectsvolume* Multiplier;
            
        }
        else
        {
            mainThrusterAudioSource.volume = 0f;
        }
        
    }

    public void PlaySmallThrusterSound(bool activate, float Multiplier = 0)
    {
        if (activate)
        {
            
       
            smallThrusterAudioSource.volume = soundEffectsvolume * Multiplier;
            
        }
        else
        {
            smallThrusterAudioSource.volume = 0f;
        }

    }




    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VLOUME_KEY, volume);
    }

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsvolume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsvolume = volume;
        PlayerPrefs.SetFloat(SOUND_EFFECTS_VLOUME_KEY, soundEffectsvolume);
    }

 

    private void OnDestroy()
    {

        Instance = null;
    }
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void LowerMusicVol()
    {
        musicAudioSource.volume = 0.2f;
    }
}
