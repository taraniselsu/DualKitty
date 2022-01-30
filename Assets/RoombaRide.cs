using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaRide : TriggerableMiniGame
{
    [SerializeField] GameObject cuteCatClimbAnimation;
    [SerializeField] SimpleFollow cam;
    bool riding = false;

    int nodeToRideTo = 1;
    [SerializeField] Transform Node1;
    [SerializeField] Transform Node2;
    [SerializeField] Transform Node3;
    [SerializeField] Transform Node4;

    public override void Start()
    {
        base.Start();
        cuteCatClimbAnimation.SetActive(false);
    }

    void Update()
    {
        if (riding == true)
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
                EndRide();

            triggerObject.transform.position = transform.position;
        }
        else
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.E))
                TriggerMinigame();
        }

        switch (nodeToRideTo)
        {
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, Node1.position, Time.deltaTime * 2);
                if (transform.position == Node1.position) nodeToRideTo = 2;
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, Node2.position, Time.deltaTime * 2);
                if (transform.position == Node2.position) nodeToRideTo = 3;
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, Node3.position, Time.deltaTime * 2);
                if (transform.position == Node3.position) nodeToRideTo = 4;
                break;
            case 4:
                transform.position = Vector3.MoveTowards(transform.position, Node4.position, Time.deltaTime * 2);
                if (transform.position == Node4.position) nodeToRideTo = 1;
                break;
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
        riding = true;

        if (AudioManager.instance)
        {
            AudioManager.instance.PlayRoombaMusic();
        }
    }

    void EndRide()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.PlayHouseMusic();
        }

        // Disable the original cat renderer
        if (triggerObject.GetComponent<HouseMovement>() != null) triggerObject.GetComponent<HouseMovement>().enabled = true;
        if (triggerObject.GetComponentInChildren<SpriteRenderer>() != null) triggerObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        triggerObject.transform.position = transform.position;
        cuteCatClimbAnimation.SetActive(false);
        cam.SetTarget(triggerObject.transform);
        riding = false;
    }
}
