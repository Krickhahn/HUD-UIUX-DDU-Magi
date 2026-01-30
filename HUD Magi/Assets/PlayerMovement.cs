using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 20f;

    [Header("Mouse Look")]
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;

    [Header("Crouch")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private KeyCode crouchKey = KeyCode.R;

    private CharacterController controller;
    private PlayerStats playerStats;          // ✅ added
    private Vector3 moveDirection;
    private float rotationX;

    private bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();   // ✅ added

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        float speed = GetCurrentSpeed();

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * inputX + transform.forward * inputZ;
        moveDirection.x = move.x * speed;
        moveDirection.z = move.z * speed;

        if (controller.isGrounded && Input.GetButton("Jump") && canMove)
        {
            moveDirection.y = jumpPower;
        }

        HandleCrouch();

        controller.Move(moveDirection * Time.deltaTime);
    }

    float GetCurrentSpeed()
    {
        // Crouch always overrides sprint
        if (Input.GetKey(crouchKey))
            return crouchSpeed;

        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool movingForward = Input.GetAxis("Vertical") > 0.1f;

        if (wantsToSprint && movingForward)
        {
            // 🔋 Drain stamina while sprinting
            bool canSprint = playerStats.UseStamina(
                playerStats.sprintDrainRate * Time.deltaTime
            );

            if (canSprint)
                return runSpeed;
        }

        return walkSpeed;
    }

    void HandleCrouch()
    {
        if (Input.GetKey(crouchKey))
            controller.height = crouchHeight;
        else
            controller.height = standingHeight;
    }

    void HandleMouseLook()
    {
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}