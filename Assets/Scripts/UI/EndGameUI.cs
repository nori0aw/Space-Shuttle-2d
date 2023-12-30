using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class EndGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button  mainMenuBtn;
    [SerializeField] TextMeshProUGUI text;
    public static EndGameUI Instance { private set; get; }

    private void Awake()
    {
        Instance = this;

        mainMenuBtn.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
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

    public void Show(string endMessage)
    {
        text.text = endMessage; 
        mainMenuBtn.Select();
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
