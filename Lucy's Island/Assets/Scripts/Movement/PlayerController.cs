using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private Vector2 currentMove;
    private bool run;
    private float currentJumpHeight;
    private bool canDoubleJump = false;
    private Vector3 velocity;

    private PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();

        inputActions.Player.Move.performed += ctx => currentMove = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => currentMove = Vector2.zero;

        inputActions.Player.Run.performed += ctx => run = true;
        inputActions.Player.Run.canceled += ctx => run = false;

        inputActions.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            currentJumpHeight = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            currentJumpHeight = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(currentMove.x, 0, currentMove.y);
        float speed = run ? runSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
