using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText, gemCountText;
    [SerializeField] Image fuel;
    [SerializeField] TextMeshProUGUI fuelText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float gamePlayingTime = SpaceShuttleGameManager.Instance.GetGamePlayingTime();

        TimeSpan time = TimeSpan.FromSeconds(gamePlayingTime);

        timeText.text = "Time: "+ time.ToString("hh':'mm':'ss");

        gemCountText.text = "Gems: "+SpaceShuttleGameManager.Instance.GetGemCount().ToString()+" / "+ SpaceShuttleGameManager.Instance.GetGemsTotalCount();

        
        fuel.fillAmount = SpaceShuttleGameManager.Instance.GetFuelLevel() / 100f;
        fuelText.text = SpaceShuttleGameManager.Instance.GetFuelLevel().ToString("F1");


    }
}
