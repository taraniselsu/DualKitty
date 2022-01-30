using UnityEngine;
using UnityEngine.InputSystem;

public class HouseMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float pushForce = 1f;

    private Vector3 moveInput = Vector3.zero;
    private bool run = false;
    private Vector3 lastMoveInput = Vector3.zero;

    private void OnValidate()
    {
        if (!controller)
        {
            controller = GetComponent<CharacterController>();
        }
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void OnMove_House(InputValue value)
    {
        // Keep track of the last movement direction
        if (moveInput != Vector3.zero)
        {
            lastMoveInput = Vector3.ClampMagnitude(moveInput, 0.1f);
        }


        Vector2 temp = value.Get<Vector2>();

        // Disallow diagonal movement for now
        if (temp.y != 0)
        {
            temp.x = 0;
        }

        moveInput = new Vector3(temp.y, 0, -temp.x); // equivalent to Quaternion.AngleAxis(90, Vector3.up) * new Vector3(temp.x, 0, temp.y)
    }

    public void OnRun_House(InputValue value)
    {
        run = value.Get<float>() > 0.5f;
    }

    private void Update()
    {
        float moveSpeed = run ? runSpeed : walkSpeed;
        Vector3 move = moveInput * moveSpeed;

        controller.SimpleMove(move);

        if (moveInput != Vector3.zero)
        {
            // animate the correct sprite for the movement direction
            animator.SetFloat("xMove", move.x);
            animator.SetFloat("zMove", move.z);
        }
        else
        {
            // change sprite to "standing" in the correct direction
            animator.SetFloat("xMove", lastMoveInput.x);
            animator.SetFloat("zMove", lastMoveInput.z);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb && !rb.isKinematic)
        {
            // Add a small random factor to the move direction
            Vector3 dir = new Vector3(
                    hit.moveDirection.x + Random.Range(-0.05f, 0.05f),
                    Random.Range(-0.05f, 0.05f),
                    hit.moveDirection.z + Random.Range(-0.05f, 0.05f)
                );
            float force = hit.moveLength / Time.deltaTime * pushForce;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity + dir * force, 10f);
        }
    }
}
