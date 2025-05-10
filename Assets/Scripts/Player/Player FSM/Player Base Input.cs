using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseInput : MonoBehaviour
{

    public PlayerInput playerInput;
    Vector2 InputDirection;
    Vector3 Movement;
    bool SprintPressed;
    bool AttackPressed;
    bool GuardPressed;
    bool SkillPressed;

    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Sprint.started += OnSprint;
        playerInput.Player.Sprint.canceled += OnSprint;
        playerInput.Player.Attack.started += OnAttack;
        playerInput.Player.Skill.started += OnSkill;
        playerInput.Player.Guard.started += OnGuard;
        playerInput.Player.Guard.performed += OnGuard;
        playerInput.Player.Guard.canceled += OnGuard;
    }

    // On event
    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        InputDirection = ctx.ReadValue<Vector2>();
        Movement.x = InputDirection.x;
        Movement.z = InputDirection.y;
    }

    void OnSprint(InputAction.CallbackContext ctx)
    {
        if(ctx.started) 
        {
            SprintPressed = true;
        }
        else if(ctx.canceled) 
        {
            SprintPressed = false;
        }
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        AttackPressed = true;
    }

    private void OnSkill(InputAction.CallbackContext ctx)
    {
        SkillPressed = true;
    }

    private void OnGuard(InputAction.CallbackContext ctx)
    {
        GuardPressed = ctx.ReadValue<float>() > 0f;
    }

    // Property
    public Vector2 PlayerInputDirection => InputDirection;
    public Vector3 PlayerMovement => Movement;
    public bool PlayerSprint => SprintPressed;
    public bool PlayerAttack => AttackPressed;
    public bool PlayerSkill => SkillPressed;
    public bool PlayerGuard => GuardPressed;
}
