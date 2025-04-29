using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BaseState currentState;

    public PlayerInput playerInput;
    CharacterController characterController;
    public PlayerAnimator playerAnimator;
    Animator animator;

    // Player Datas
    PlayerHealth playerHealth;

    // Effect for Attack and Skill
    [SerializeField] GameObject trailObject;

    public Vector2 InputDirection { get; private set; }
    public bool DodgePressed { get; private set; }
    public bool AttackPressed { get; private set;}
    public bool SkillPressed { get; private set; }
    public bool GuardPressed { get; private set; }
    public bool SprintPressed { get; private set; }
    public float finalSpeed { get; private set; }
    public bool isSkill { get; set; }

    // Speed value
    float moveSpeed = 8f;
    float sprintSpeed = 12f;
    float rotationSpeed = 30f;

    // Guard Orbit value
    [SerializeField] GameObject orbitObject;
    float orbitRadius = 0.8f; // radius
    float orbitDegree; // degree
    float orbitSpeed = 400f; // orbit speed

    // Hit value
    public bool isHit { get; set; }

    Vector3 currentMovement;
    public Vector3 localMovement { get; private set; }

    void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);
        characterController = GetComponent<CharacterController>();
        
        playerHealth = GetComponent<PlayerHealth>();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Sprint.started += OnSprint;
        playerInput.Player.Sprint.canceled += OnSprint;
        // playerInput.Player.Enteraction.started += OnDodge;
        // playerInput.Player.Enteraction.canceled += OnDodge;
        playerInput.Player.Attack.started += OnAttack;
        // playerInput.Player.Attack.canceled += OnAttack;
        playerInput.Player.Skill.started += OnSkill;
        // playerInput.Player.Skill.canceled += OnSkill;
        playerInput.Player.Guard.started += OnGuard;
        playerInput.Player.Guard.performed += OnGuard;
        playerInput.Player.Guard.canceled += OnGuard;

        isHit = false;

        playerInput.Enable();
    }

    void Start()
    {
        // ChangeState(new IdleState(this));
        ChangeState(new MoveState(this));

        // Initailize TrailObject
        trailObject = GameObject.FindWithTag("Attack Trail");
        orbitObject = GameObject.FindWithTag("Guard Trail");
        
        EndTrail();
        EndOrbitTrail();
    }

    void Update()
    {
        currentState?.Update();

        DodgePressed = false;
        AttackPressed = false;
        SkillPressed = false;
        // GuardPressed = false;

        LocalMoveDir();
    }

    // Methods
    public void PlayerMove(float multipleSpeed)
    {
        // Little Gravity on Player
        currentMovement.y = -0.5f; 

        // Speed Calculate
        finalSpeed = SprintPressed ? sprintSpeed : moveSpeed;
        
        // Skill atcivate speed
        var skillSpeed = isSkill ? (SprintPressed ? 1f : 2f) : 1f;

        // Move Player
        characterController.Move(currentMovement * finalSpeed * skillSpeed * multipleSpeed * Time.deltaTime);
    }

    public void PlayerRotation()
    {
        Vector3 direction = new Vector3(currentMovement.x, 0, currentMovement.z);
        
        // If vector is zero, dont rotate
        if(direction.sqrMagnitude < 0.001f) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * (SprintPressed ? rotationSpeed : rotationSpeed / 2f));
    }

    // https://www.youtube.com/watch?v=XI56ogm7eFI
    public void PlayerMouseRotation()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 direction = ray.GetPoint(distance) - transform.position;
            direction.y = 0f;

            // If vector is zero, dont rotate
            if (direction.sqrMagnitude < 0.001f)
                return;

            // transform.rotation = Quaternion.LookRotation(direction);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * (SprintPressed ? rotationSpeed : rotationSpeed / 2f));
        }
    }

    public void LocalMoveDir()
    {
        localMovement = this.transform.InverseTransformDirection(new Vector3(currentMovement.x, 0, currentMovement.z));
    }

    // https://sharp2studio.tistory.com/4
    public void OrbitRotation()
    {
        orbitDegree += Time.deltaTime * orbitSpeed;

        if(orbitRadius >= 360f) orbitDegree -= 360f;

        float rad = Mathf.Deg2Rad * (orbitDegree);
        float x = orbitRadius * Mathf.Cos(rad);
        float z = orbitRadius * Mathf.Sin(rad);

        orbitObject.transform.position = this.transform.position + new Vector3(x, 0.5f, z);
        // orbitObject.transform.rotation = Quaternion.Euler(0, 0, orbitDegree * -1); // 가운데를 바라보게 각도 조절
        // orbitObject.transform.rotation = Quaternion.LookRotation(this.transform.position - orbitObject.transform.position);
    }

    public void StartTrail()
    {
        trailObject.SetActive(true);
    }

    public void EndTrail()
    {
        trailObject.SetActive(false);
    }

    public void StartOrbitTrail()
    {
        orbitObject.SetActive(true);
    }

    public void EndOrbitTrail()
    {
        orbitObject.SetActive(false);
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

    public IEnumerator HitColor()
    {
        yield return new WaitForSeconds(0);
    }

    public PlayerAnimator PlayerAnimator => playerAnimator;
    public Animator Animator => animator;
}