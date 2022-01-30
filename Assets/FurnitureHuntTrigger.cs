using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureHuntTrigger : TriggerableMiniGame
{

    [SerializeField] GameObject cuteCatClimbAnimation;

    float loadSceneTimer = 0.0f;
    float loadSceneTime = 6.0f;
    bool loadingScene = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        cuteCatClimbAnimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
            TriggerMinigame();

        if (loadingScene)
        {
            loadSceneTimer += Time.deltaTime;
            if (loadSceneTimer > loadSceneTime)
            {
                SceneChanger.instance.LoadFurnitureHuntScene();
            }
        }
    }

    public override void TriggerMinigame()
    {
        base.TriggerMinigame();

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMischievousDittySFX();
        // Disable the original cat renderer
        if (triggerObject.GetComponent<HouseMovement>() != null) triggerObject.GetComponent<HouseMovement>().enabled = false;
        if (triggerObject.GetComponentInChildren<SpriteRenderer>() != null) triggerObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        // Play the cat climbing tree animation
        cuteCatClimbAnimation.SetActive(true);
        // Cache the load point with the JamGameManager
        JamGameManager.instance.lastCatMinigame = LastCatMinigame.FurnitureHunt;
        // Trigger the cat tree scene after a period of time
        loadingScene = true;
    }
}
