using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] Button easy, normal, hard, exit;

    [SerializeField] TextMeshProUGUI bestScoreTxt;
    private void Awake()
    {
        easy.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.Game, 1);
        });

        normal.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.Game, 2);
        });

        hard.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.Game, 3);
        });

        exit.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    void Start()
    {
        easy.Select();

        string difficulty= "";
        float lastBestTime = -1f;


        // to Display Best Score
        for (int i = 3; i > 0 ; i--) 
        {
            lastBestTime = PlayerPrefs.GetFloat(i.ToString(), -1f);
            if (lastBestTime > 0)
            {
                
                switch (i)
                {
                    case 1:
                        difficulty = "Easy";
                        break;
                    case 2:
                        difficulty = "Normal";
                        break;
                    case 3:
                        difficulty = "Hard";
                        break;
                    default:
                        Debug.LogError("DefficultyLevel(" + SceneLoader.DefficultyLevel + ")is wrong");
                        difficulty = "NA";
                        break;
                }
                break;   
            } 
        }
        TimeSpan time = TimeSpan.FromSeconds(lastBestTime);
        bestScoreTxt.text = "Best Score: " + difficulty + " " + time.ToString(@"mm\:ss");
        /////////////////////////////////////////////////////////
        ///

    }


}
