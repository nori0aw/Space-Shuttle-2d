using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button resumeBtn, optionsBtn, mainMenuBtn;
    public static PauseGameUI Instance { private set; get; }

    private void Awake()
    {
        Instance = this;

        resumeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            SpaceShuttleGameManager.Instance.TogglePauseGame();
        });

        mainMenuBtn.onClick.AddListener(() =>
        {
            SpaceShuttleGameManager.Instance.TogglePauseGame();
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });

        optionsBtn.onClick.AddListener(() =>
        {
            SoundOptionsUI.Instance.Show();
            Hide();
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
        resumeBtn.Select();
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
