using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneCallback : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        SceneLoader.LoadSceneCallback();
    }
}
