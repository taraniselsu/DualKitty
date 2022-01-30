using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomiesTriggerableMinigame : TriggerableMiniGame
{
    float loadSceneTimer = 0.0f;
    float loadSceneTime = 5.5f;
    bool loadingScene = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
            TriggerMinigame();

        if (loadingScene)
        {
            loadSceneTimer += Time.deltaTime;
            if (loadSceneTimer > loadSceneTime)
            {
                SceneChanger.instance.LoadRaceScene();
            }
        }
    }

    public override void TriggerMinigame()
    {
        base.TriggerMinigame();

        // Disable the original cat renderer
        if (triggerObject.GetComponent<HouseMovement>() != null) triggerObject.GetComponent<HouseMovement>().enabled = false;
        //if (triggerObject.GetComponentInChildren<SpriteRenderer>() != null) triggerObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        // Play the cat climbing tree animation
        if (triggerObject.GetComponent<ZoomyModeBeforeRace>() != null) triggerObject.GetComponent<ZoomyModeBeforeRace>().TriggerZooms();
        // Cache the load point with the JamGameManager
        JamGameManager.instance.lastCatMinigame = LastCatMinigame.ZoomiesRace;
        // Trigger the cat tree scene after a period of time
        loadingScene = true;

        AudioManager.instance.PlayVroomVroomMusic();
    }
}
