using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenuScene,
        Game,
        LoadingScene
    }

    public static int DefficultyLevel = 1;

    private static Scene targetScene;
    public static void LoadScene(Scene targetScene, int Defficulty)
    {
        DefficultyLevel = Defficulty;
        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadScene(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadSceneCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
