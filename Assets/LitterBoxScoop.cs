using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterBoxScoop : TriggerableMiniGame
{
    [SerializeField] GameObject cuteCatClimbAnimation;
    [SerializeField] SimpleFollow cam;
    bool scooping = false;

    [SerializeField] MeshRenderer turd1;
    [SerializeField] MeshRenderer turd2;
    [SerializeField] MeshRenderer turd3;
    public override void Start()
    {
        base.Start();
        cuteCatClimbAnimation.SetActive(false);
        Debug.Log("Litter start");
    }

    void Update()
    {
        if (scooping == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
                EndRide();

            turd1.gameObject.transform.Translate(0, -Time.deltaTime * .004f, 0, Space.World);
            turd2.gameObject.transform.Translate(0, -Time.deltaTime * .004f, 0, Space.World);
            turd3.gameObject.transform.Translate(0, -Time.deltaTime * .004f, 0, Space.World);
            triggerObject.transform.position = transform.position;
        }
        else
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.E))
                TriggerMinigame();
        }
    }


    public override void TriggerMinigame()
    {
        base.TriggerMinigame();

        

        cam.SetTarget(transform);
        // Disable the original cat renderer
        if (triggerObject.GetComponent<HouseMovement>() != null) triggerObject.GetComponent<HouseMovement>().enabled = false;
        if (triggerObject.GetComponentInChildren<SpriteRenderer>() != null) triggerObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        // Play the cat climbing tree animation
        cuteCatClimbAnimation.SetActive(true);
        // Cache the load point with the JamGameManager
        scooping = true;

        if (AudioManager.instance)
        {
            //AudioManager.instance.PlayRoombaMusic();
        }
    }

    void EndRide()
    {
        if (AudioManager.instance)
        {
            //AudioManager.instance.PlayHouseMusic();
        }

        // Disable the original cat renderer
        if (triggerObject.GetComponent<HouseMovement>() != null) triggerObject.GetComponent<HouseMovement>().enabled = true;
        if (triggerObject.GetComponentInChildren<SpriteRenderer>() != null) triggerObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        triggerObject.transform.position = transform.position;
        cuteCatClimbAnimation.SetActive(false);
        cam.SetTarget(triggerObject.transform);
        scooping = false;
    }
}
