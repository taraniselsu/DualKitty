using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomiesManager : MonoBehaviour
{
    [SerializeField] ZoomiesPlayer player;
    [SerializeField] new Camera camera;

    [SerializeField] ZoomiesObstacle templateChairLeg;
    [SerializeField] GameObject folderForObstacles;

    [SerializeField] ZoomiesGround templateGround;
    [SerializeField] GameObject folderForGround;

    [SerializeField] ZoomiesLava templateLava;
    [SerializeField] GameObject folderForLava;

    [SerializeField] ZoomiesGroundMaterials groundMaterials;

    [SerializeField] ZoomiesHUD hud;

    public bool isRaceRunning {get; private set;}

    private Queue<ZoomiesObstacle> obstacles = new Queue<ZoomiesObstacle>();
    private Queue<ZoomiesGround> nonFirstGrounds = new Queue<ZoomiesGround>();
    private Queue<ZoomiesLava> nonFirstLavas = new Queue<ZoomiesLava>();

    private Vector2 chairLegSpawnRangeX = new Vector2(-5f, 5f);

    private const float Z_OF_FIRST_OBSTACLE = -15f;
    private const float DIST_BETWEEN_OBSTACLE = 30f;
    private const float OBSTACLE_OFFSCREEN_DIST = 80f;
    private const int NUM_OBSTACLES_AHEAD = 4;
    private float zOfMostRecentObstacle = 0f;

    private const float Z_OF_FIRST_GROUND = 0f;
    private const float DIST_BETWEEN_GROUND = 40f;
    private const float GROUND_OFFSCREEN_DIST = 80f;
    private const int NUM_GROUNDS_AHEAD = 2;
    private float zOfMostRecentGround = 0f;

    private const float Z_OF_FIRST_LAVA = 0f;
    private const float DIST_BETWEEN_LAVA = 40f;
    private const float LAVA_OFFSCREEN_DIST = 80f;
    private const int NUM_LAVAS_AHEAD = 2;
    private float zOfMostRecentLava = 0f;

    private const float CAMERA_HEIGHT = 1.93f;
    private const float CAMERA_FOLLOW_DIST = 6.17f;

    int nextGroundColor = 0;

    private const float PRE_RACE_DELAY_SECS = 0.2f;

    private const float COLLISION_PENALTY_SECS = 1f;

    private const float POINTS_PER_DISTANCE = 10f;

    private float zoomiesStartTime = float.MaxValue;
    private float MAX_ENERGY_IN_SECONDS = 30;
    private float distanceTraveledThisRun = 0f;
    private float distanceFromPreviousRuns = 0f;

    private const float SECS_DELAY_BEFORE_RETURN_TO_HOUSE = 5f;

    private const float LAVA_X_ABS = 55.5f;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Launching Zoomies Race");
        if (AudioManager.instance) {
            AudioManager.instance.PlayExcitingRaceMusic();
        }
        isRaceRunning = false;
        zoomiesStartTime = Time.time;

        this.InvokeAction(BeginRace, PRE_RACE_DELAY_SECS);
    }

    // Update is called once per frame
    void Update()
    {
        var newCameraPos = new Vector3(0, CAMERA_HEIGHT, player.transform.position.z - CAMERA_FOLLOW_DIST);
        camera.transform.position = newCameraPos;

        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log($"Zoomies reset key pressed");
            FullReset();
        }

        var playerPos = player.transform.position.z;
        distanceTraveledThisRun = playerPos;

        if (playerPos > zOfMostRecentGround - DIST_BETWEEN_GROUND * NUM_GROUNDS_AHEAD) {
            InstantiateNewGround();
        }
        if (nonFirstGrounds.Count > 0) {
            while (nonFirstGrounds.Peek().transform.position.z + GROUND_OFFSCREEN_DIST < playerPos) {
                var thisGround = nonFirstGrounds.Dequeue();
                Destroy(thisGround.gameObject);
            }
        }

        if (playerPos > zOfMostRecentLava - DIST_BETWEEN_LAVA * NUM_LAVAS_AHEAD)
        {
            InstantiateNewLava();
        }
        if (nonFirstLavas.Count > 0)
        {
            while (nonFirstLavas.Peek().transform.position.z + LAVA_OFFSCREEN_DIST < playerPos)
            {
                var thisLava = nonFirstLavas.Dequeue();
                Destroy(thisLava.gameObject);
            }
        }

        if (playerPos > zOfMostRecentObstacle - DIST_BETWEEN_OBSTACLE * NUM_OBSTACLES_AHEAD)
        {
            InstantiateNewChairLeg();
        }
        if (obstacles.Count > 0)
        {
            while (obstacles.Peek().transform.position.z + OBSTACLE_OFFSCREEN_DIST < playerPos)
            {
                var thisObstacle = obstacles.Dequeue();
                Destroy(thisObstacle.gameObject);
            }
        }

        var secsInZoomies = Time.time - zoomiesStartTime;
        if (secsInZoomies >= MAX_ENERGY_IN_SECONDS) {
            hud.SetEnergyFill(0f);
            OutOfEnergy();
        } else {
            var fracEnergyDrained = secsInZoomies / MAX_ENERGY_IN_SECONDS;
            var fracEnergyRemaining = 1 - fracEnergyDrained;
            hud.SetEnergyFill(fracEnergyRemaining);
        }

        UpdateScore();
    }

    private void UpdateScore() {
        var pointsFloat = (distanceTraveledThisRun + distanceFromPreviousRuns) * POINTS_PER_DISTANCE;
        var pointsInt = (int)pointsFloat;
        hud.SetScore(pointsInt);
    }

    private void OutOfEnergy() {
        Debug.Log($"Zoomies out of energy");
        isRaceRunning = false;
        player.ForcedStop();
        // TODO: Probably do some other game over stuff

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayHeavyDrumBeatsSFX();
        this.InvokeAction(ReturnHome, SECS_DELAY_BEFORE_RETURN_TO_HOUSE);
    }

    private void ReturnHome() {
        SceneChanger.instance.LoadGameScene();
    }

    private void IncrementNextColorIndex() {
        nextGroundColor++;
        if (nextGroundColor >= groundMaterials.materials.Count) {
            nextGroundColor = 0;
        }
    }

    private void InstantiateNewChairLeg()
    {
        zOfMostRecentObstacle += DIST_BETWEEN_OBSTACLE;
        var spawnX = Random.Range(chairLegSpawnRangeX.x, chairLegSpawnRangeX.y);

        var newLeg = Instantiate(templateChairLeg, new Vector3(spawnX, 0, zOfMostRecentObstacle), Quaternion.identity);
        newLeg.gameManager = this;
        newLeg.transform.SetParent(folderForObstacles.transform);
        obstacles.Enqueue(newLeg);
    }

    private void InstantiateNewGround()
    {
        zOfMostRecentGround += DIST_BETWEEN_GROUND;
        var newGround = Instantiate(templateGround, new Vector3(0, 0, zOfMostRecentGround), Quaternion.identity);
        newGround.gameManager = this;
        newGround.transform.SetParent(folderForGround.transform);
        AssignMaterialAndIncrementMaterialCounter(newGround);
        nonFirstGrounds.Enqueue(newGround);
    }

    private void InstantiateNewLava()
    {
        zOfMostRecentLava += DIST_BETWEEN_LAVA;
        var newLavaL = Instantiate(templateLava, new Vector3(LAVA_X_ABS, 0, zOfMostRecentLava), Quaternion.identity);
        newLavaL.gameManager = this;
        newLavaL.transform.SetParent(folderForLava.transform);
        nonFirstLavas.Enqueue(newLavaL);
        var newLavaR = Instantiate(templateLava, new Vector3(-LAVA_X_ABS, 0, zOfMostRecentLava), Quaternion.identity);
        newLavaR.gameManager = this;
        newLavaR.transform.SetParent(folderForLava.transform);
        nonFirstLavas.Enqueue(newLavaR);
    }

    private void AssignMaterialAndIncrementMaterialCounter(ZoomiesGround newGround) {
        newGround.SetMaterial(groundMaterials.materials[nextGroundColor]);
        IncrementNextColorIndex();
    }

    private void BeginRace()
    {
        isRaceRunning = true;
        Debug.Log($"Zoomies race go!");
    }

    public void CollisionDetected() {
        Debug.Log($"Zoomies Manager collision heard");
        isRaceRunning = false;
        player.ForcedStop();
        this.InvokeAction(Reset, COLLISION_PENALTY_SECS);
    }

    private void FullReset() {
        isRaceRunning = false;
        Reset();
        distanceFromPreviousRuns = 0f;
        this.InvokeAction(BeginRace, PRE_RACE_DELAY_SECS);
        zoomiesStartTime = Time.time;
    }

    private void Reset()
    {
        Debug.Log($"Zoomies reset");
        distanceFromPreviousRuns += distanceTraveledThisRun;
        distanceTraveledThisRun = 0f;
        player.ResetPositionAndRotation();

        foreach(var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }
        obstacles.Clear();
        zOfMostRecentObstacle = Z_OF_FIRST_OBSTACLE;

        foreach (var lava in nonFirstLavas)
        {
            Destroy(lava.gameObject);
        }
        nonFirstLavas.Clear();
        zOfMostRecentLava = Z_OF_FIRST_LAVA;

        foreach (var ground in nonFirstGrounds)
        {
            Destroy(ground.gameObject);
        }
        nonFirstGrounds.Clear();
        nextGroundColor = 0;
        zOfMostRecentGround = Z_OF_FIRST_GROUND;

        InstantiateNewGround();
        InstantiateNewLava();
        this.InvokeAction(BeginRace, PRE_RACE_DELAY_SECS);
    }
}
