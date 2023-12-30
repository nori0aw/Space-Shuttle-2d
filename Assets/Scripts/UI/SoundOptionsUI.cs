using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionsUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Slider musicSlider, soundFXSlider;
    public static SoundOptionsUI Instance { private set; get; }

    private void Awake()
    {
        Instance = this;

        resumeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            SpaceShuttleGameManager.Instance.TogglePauseGame();
        });

        musicSlider.onValueChanged.AddListener((float volume) =>
        {
            SoundManager.Instance.SetMusicVolume(volume);
        });

        soundFXSlider.onValueChanged.AddListener((float volume) =>
        {
            SoundManager.Instance.SetSoundEffectsVolume(volume);
        });

    }
    void Start()
    {

        gameObject.SetActive(false);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {

        musicSlider.value =  SoundManager.Instance.GetMusicVolume();
        soundFXSlider.value = SoundManager.Instance.GetSoundEffectsVolume();

        musicSlider.Select();

        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
