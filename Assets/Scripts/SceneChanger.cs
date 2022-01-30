using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [SerializeField] string menuSceneName;
    [SerializeField] string howToPlaySceneName;
    [SerializeField] string creditsSceneName;
    [SerializeField] string gameSceneName;
    [Space(10)]
    [SerializeField] string windowSceneName;
    [SerializeField] string raceSceneName;
    [SerializeField] string catTreeSceneName;
    [SerializeField] string furnitureHuntSceneName;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene(howToPlaySceneName);
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void LoadWindowScene()
    {
        SceneManager.LoadScene(windowSceneName);
    }
    public void LoadRaceScene()
    {
        SceneManager.LoadScene(raceSceneName);
    }
    public void LoadCatTreeScene()
    {
        SceneManager.LoadScene(catTreeSceneName);
    }
    public void LoadFurnitureHuntScene()
    {
        SceneManager.LoadScene(furnitureHuntSceneName);
    }
}
