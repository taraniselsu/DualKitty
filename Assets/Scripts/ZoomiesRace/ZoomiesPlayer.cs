using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ZoomiesPlayer : MonoBehaviour
{
    [SerializeField] ZoomiesManager gameManager;
    [SerializeField] private CharacterController controller;
    [SerializeField] Transform physicalTransform;
    [SerializeField] Transform carSpriteTransform;

    // Assumed: Freeze Position is unticked for all 3 axes
    // Assumed: Freeze Rotation is ticked for all 3 axes
    // Assumed: Kinematic is unticked. We'll probably tick it via code. This is
    // to ensure that the code can position the block exactly, whereas kinematic
    // objects don't always get positioned exactly.
    [SerializeField] new Rigidbody rigidbody;

    [SerializeField] new Collider collider;

    private CharacterController characterController;

    Vector3 V3_RACE_START_PHYSICAL = new Vector3(0, 0.51f, 0);

    float TOP_SPEED = 80f;
    float ACCEL = 400f;
    float DECEL = -200f;

    float TOP_LATERAL_SPEED = 10f;
    float LATERAL_ACCEL = 400f;

    private const float MAX_CAR_SPRITE_ROT_ABS_DEGS = 30f;

    Vector3 COLLISION_VELOCITY = new Vector3(0, 5, 200);
    Vector3 COLLISION_ANGULAR_VELOCITY_DEG = new Vector3(-720, 30, 0);

    float move;
    bool jump;

    private void OnValidate()
    {
        if (!controller)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    public void OnMoveLateral(InputValue value)
    {
        move = value.Get<float>();
        Debug.Log($"move: {move}");
    }

    public void OnAccelerate()
    {
        jump = true;
        Debug.Log($"jump: {jump}");
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ResetPositionAndRotation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (gameManager.isRaceRunning) {
            float deltaVelocity = 0f;
            if (Input.GetKey(KeyCode.W)) {
                // Debug.Log($"Accelerating");
                deltaVelocity = ACCEL;
            } else {
                // Debug.Log($"Not accelerating");
                deltaVelocity = DECEL;
            }
            float deltaLateralSpeed = 0f;
            float newXVelocityBounded = 0f;
            // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            //     if (Input.GetKey(KeyCode.A)) {
            if (move != 0f) {
                if (move < 0f) {
                    deltaLateralSpeed = -LATERAL_ACCEL;
                // } else if (Input.GetKey(KeyCode.D)) {
                } else if (move > 0f) {
                    deltaLateralSpeed = LATERAL_ACCEL;
                }
                var newXVelocityUnbounded = rigidbody.velocity.x + deltaLateralSpeed * Time.fixedDeltaTime;
                if (Mathf.Sign(newXVelocityUnbounded) > 0) {
                    newXVelocityBounded = Mathf.Clamp(newXVelocityUnbounded, 0, TOP_LATERAL_SPEED);
                } else {
                    newXVelocityBounded = Mathf.Clamp(newXVelocityUnbounded, -TOP_LATERAL_SPEED, 0);
                }
                if (Mathf.Sign(newXVelocityUnbounded) > 0) {
                    newXVelocityBounded = Mathf.Clamp(newXVelocityUnbounded, 0, TOP_LATERAL_SPEED);
                } else {
                    newXVelocityBounded = Mathf.Clamp(newXVelocityUnbounded, -TOP_LATERAL_SPEED, 0);
                }
            } else {
                newXVelocityBounded = 0;
            }

            var newZVelocityUnbounded = rigidbody.velocity.z + deltaVelocity * Time.fixedDeltaTime;
            var newZVelocityBounded = Mathf.Clamp(newZVelocityUnbounded, 0, TOP_SPEED);
            var desiredVelocityVector = new Vector3(newXVelocityBounded, 0, newZVelocityBounded);
            var velocityDifference = desiredVelocityVector - rigidbody.velocity;
            rigidbody.AddForce(velocityDifference, ForceMode.VelocityChange);
            RotateCarSprite(newXVelocityBounded);
        } else {
            RotateCarSprite(0);
        }
    }

    private void RotateCarSprite(float xVelocity) {
        var signedVelocityRatio = xVelocity / TOP_LATERAL_SPEED;
        var zRotDegrees = -1 * signedVelocityRatio * MAX_CAR_SPRITE_ROT_ABS_DEGS;
        carSpriteTransform.eulerAngles = new Vector3(0, 0, zRotDegrees);
    }

    public void ForcedStop() {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
        rigidbody.isKinematic = false;
        Debug.Log($"Zoomies player stopped!");
    }

    public void ResetPositionAndRotation() {
        //ForcedStop();
        rigidbody.isKinematic = true;
        rigidbody.position.Set(V3_RACE_START_PHYSICAL.x, V3_RACE_START_PHYSICAL.y, V3_RACE_START_PHYSICAL.z);
        physicalTransform.localPosition = V3_RACE_START_PHYSICAL;
        rigidbody.isKinematic = false;
        Debug.Log($"Zoomies player position reset!");
    }

    private void OnCollisionEnter(Collision other) {
        if (!gameManager.isRaceRunning) {return;}
        if (other.gameObject.name == "PhysicalGround") {return;}

        Debug.Log($"Zoomies Player collision detected with {other.gameObject.name}");

        gameManager.CollisionDetected();

        if (AudioManager.instance) {
            AudioManager.instance.PlayReeowShortSFX();
        }

        if (other.gameObject.name != "PhysicalLava") {
            var collisionVelocityInRads = COLLISION_ANGULAR_VELOCITY_DEG * Mathf.Deg2Rad;
            other.rigidbody.angularVelocity = collisionVelocityInRads;
            other.rigidbody.velocity = COLLISION_VELOCITY;
        }
    }
}
