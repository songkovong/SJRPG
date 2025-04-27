using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BaseState currentState;

    public PlayerInput playerInput;
    CharacterController characterController;
    public PlayerAnimator playerAnimator { get; private set; }
    Animator animator;

    public Vector2 InputDirection { get; private set; }
    public bool DodgePressed { get; private set; }
    public bool AttackPressed { get; private set;}
    public bool SkillPressed { get; private set; }
    public bool GuardPressed { get; private set; }
    public bool SprintPressed { get; private set; }
    public float finalSpeed { get; private set; }
    public bool isSkill { get; set; }

    // Speed value
    float moveSpeed = 5f;
    float sprintSpeed = 10f;
    float rotationSpeed = 20f;

    Vector3 currentMovement;

    void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);
        characterController = GetComponent<CharacterController>();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Sprint.started += OnSprint;
        playerInput.Player.Sprint.canceled += OnSprint;
        // playerInput.Player.Enteraction.started += OnDodge;
        // playerInput.Player.Enteraction.canceled += OnDodge;
        playerInput.Player.Attack.started += OnAttack;
        playerInput.Player.Attack.canceled += OnAttack;
        playerInput.Player.Skill.started += OnSkill;
        playerInput.Player.Skill.canceled += OnSkill;
        playerInput.Player.Guard.started += OnGuard;
        playerInput.Player.Guard.performed += OnGuard;
        playerInput.Player.Guard.canceled += OnGuard;

        playerInput.Enable();
    }

    void Start()
    {
        // ChangeState(new IdleState(this));
        ChangeState(new MoveState(this));
    }

    void Update()
    {
        currentState?.Update();

        DodgePressed = false;
        AttackPressed = false;
        SkillPressed = false;
        // GuardPressed = false;
    }

    // Methods
    public void PlayerMove()
    {
        // Little Gravity on Player
        currentMovement.y = -0.5f; 

        // Speed Calculate
        finalSpeed = SprintPressed ? sprintSpeed : moveSpeed;
        
        // Skill atcivate speed
        var skillSpeed = isSkill ? (SprintPressed ? 1f : 2f) : 1f;

        // Move Player
        characterController.Move(currentMovement * finalSpeed * skillSpeed * Time.deltaTime);
    }

    public void PlayerRotation()
    {
        Vector3 direction = new Vector3(currentMovement.x, 0, currentMovement.z);
        
        // If vector is zero, dont rotate
        if(direction.sqrMagnitude < 0.001f) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * (SprintPressed ? rotationSpeed : rotationSpeed / 2));
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // On event
    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        InputDirection = ctx.ReadValue<Vector2>();
        currentMovement.x = InputDirection.x;
        currentMovement.z = InputDirection.y;
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

    // private void OnDodge(InputAction.CallbackContext ctx)
    // {
    //     DodgePressed = true;
    // }

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

    // Coroutine
    public void StartCoroutinePlayer(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public PlayerAnimator PlayerAnimator => playerAnimator;
    public Animator Animator => animator;
}