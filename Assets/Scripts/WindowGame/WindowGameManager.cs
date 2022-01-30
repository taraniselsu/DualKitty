using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGameManager : MonoBehaviour
{
    float everythingTheLightTouchesTime = 3.0f;
    bool everythingTheLightTouchesTriggered = false;
    float gameTimer = 0.0f;
    float backToHouseSceneTimer = 16.5f;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayGloriousSFX();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer >= everythingTheLightTouchesTime && !everythingTheLightTouchesTriggered) {
            //AudioManager.instance.PlayEverythingTheLightTouchesSFX();
            everythingTheLightTouchesTriggered = true;
        }

        if (gameTimer >= backToHouseSceneTimer)
            SceneChanger.instance.LoadGameScene();
    }
}
