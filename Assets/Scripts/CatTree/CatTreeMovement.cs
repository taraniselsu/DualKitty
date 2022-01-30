using UnityEngine;
using UnityEngine.InputSystem;

public class CatTreeMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravityValue = -1f;

    private float move;
    private bool jump;
    private Vector2 playerVelocity;

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
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void OnMove_CatTree(InputValue value)
    {
        move = value.Get<float>();
    }

    public void OnJump_CatTree(InputValue value)
    {
        jump = value.Get<float>() > 0.5f;
    }

    private void Update()
    {
        Vector3 oldPos = transform.position;

        bool isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (jump)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jump = false;

                if (AudioManager.instance)
                    AudioManager.instance.PlayJumpSFX();
            }
        }

        playerVelocity.x = move * playerSpeed;
        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);


        Vector3 deltaPos = transform.position - oldPos;
        bool isRunning = deltaPos.x != 0;

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("xMove", deltaPos.x);
        animator.SetFloat("yMove", deltaPos.y);

        if (deltaPos.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (deltaPos.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (playerVelocity.y > 0 && deltaPos.y <= 0)
        {
            playerVelocity.y = 0;
        }
    }
}
