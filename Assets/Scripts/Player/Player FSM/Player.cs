using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BaseState currentState;

    public PlayerInput playerInput;
    CharacterController characterController;
    PlayerAnimator playerAnimator;
    Animator animator;


    HitColor playerHitColor;
    PlayerGuardTrail _guardTrail;
    PlayerAttackTrail _attackTrail;
    SwordHitbox _attackHitbox;


    // Player Stat
    public PlayerStat playerStat { get; private set; }


    // Player Input Value
    public Vector2 InputDirection { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool SpaceSkillPressed { get; private set; }
    public bool CSkillPressed { get; private set; }
    public bool RSkillPressed { get; private set; }
    public bool GuardPressed { get; private set; }
    public bool SprintPressed { get; private set; }
    public bool StatPressed { get; private set; }
    public bool ItemPressed { get; private set; }
    public bool ClosePressed { get; private set; }
    public bool EnteractionPressed { get; private set; }

    public bool DontRotate { get; set; }


    public float finalSpeed { get; private set; }
    public bool isSkill { get; set; }

    // Hit value
    public bool isHit { get; set; }

    // Skill value
    public int skillCode { get; set; }
    public float skillDamage { get; set; }

    // Weapon value
    public int weaponCode { get; set; }

    // Dead Value
    public bool isDead { get; set; } = false;

    // Damage Text
    public GameObject damageText;
    public Transform damagePos;

    Vector3 currentMovement;
    public Vector3 localMovement { get; private set; }

    void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);
        characterController = GetComponent<CharacterController>();

        playerStat = GetComponent<PlayerStat>();

        playerHitColor = GetComponent<HitColor>();
        _guardTrail = GetComponentInChildren<PlayerGuardTrail>();
        _attackTrail = GetComponentInChildren<PlayerAttackTrail>();
        _attackHitbox = GetComponentInChildren<SwordHitbox>();


        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Sprint.started += OnSprint;
        playerInput.Player.Sprint.canceled += OnSprint;
        playerInput.Player.Attack.started += OnAttack;
        playerInput.Player.Skill1.started += OnSpaceSkill;
        playerInput.Player.Skill2.started += OnCSkill;
        playerInput.Player.Skill3.started += OnRSkill;
        playerInput.Player.Guard.started += OnGuard;
        playerInput.Player.Guard.performed += OnGuard;
        playerInput.Player.Guard.canceled += OnGuard;
        playerInput.Player.Enteraction.started += OnEnteraction;
        playerInput.Player.Enteraction.canceled += OnEnteraction;

        isHit = false;

        playerInput.Enable();
        // playerInput.Player.Disable();
    }

    void Start()
    {
        ChangeState(new MoveState(this));
    }

    void Update()
    {
        currentState?.Update();

        AttackPressed = false;
        SpaceSkillPressed = false;
        CSkillPressed = false;
        RSkillPressed = false;
        // EnteractionPressed = false;

        LocalMoveDir();

        Debug.Log("Skill Code = " + skillCode);
    }

    // Methods
    public void PlayerMove(float multipleSpeed)
    {
        // Little Gravity on Player
        currentMovement.y = -0.5f;

        // Speed Calculate
        // finalSpeed = SprintPressed ? sprintSpeed : moveSpeed;
        // finalSpeed = SprintPressed ? playerStat.sprintSpeed : playerStat.moveSpeed;
        finalSpeed = isSkill ? playerStat.data.sprintSpeed : SprintPressed ? playerStat.data.sprintSpeed : playerStat.data.moveSpeed;
        // if add skillSpeed, fix sprintSpeed part;

        // Move Player
        characterController.Move(currentMovement * finalSpeed * multipleSpeed * Time.deltaTime);
    }

    public void PlayerRotation()
    {
        Vector3 direction = new Vector3(currentMovement.x, 0, currentMovement.z);

        // If vector is zero, dont rotate
        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * (SprintPressed ? rotationSpeed : rotationSpeed / 2f));
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation,
            targetRotation,
            Time.deltaTime * (SprintPressed ? playerStat.data.rotationSpeed : playerStat.data.rotationSpeed / 2f)
        );
    }

    // https://www.youtube.com/watch?v=XI56ogm7eFI
    public void PlayerMouseRotation()
    {
        if(!DontRotate)
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
                // this.transform.rotation = Quaternion.Slerp(
                //     this.transform.rotation,
                //     targetRotation,
                //     Time.deltaTime * (SprintPressed ? rotationSpeed : rotationSpeed / 2f)
                // );
                this.transform.rotation = Quaternion.Slerp(
                    this.transform.rotation,
                    targetRotation,
                    Time.deltaTime * (SprintPressed ? playerStat.data.rotationSpeed : playerStat.data.rotationSpeed / 2f)
                );
            }
        }
    }

    public void LocalMoveDir()
    {
        localMovement = this.transform.InverseTransformDirection(new Vector3(currentMovement.x, 0, currentMovement.z));
    }

    public void ChangeState(BaseState newState)
    {
        // If game is pause, dont change state
        if (!GameManager.instance.isPaused)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
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
        if (ctx.started)
        {
            SprintPressed = true;
        }
        else if (ctx.canceled)
        {
            SprintPressed = false;
        }
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        AttackPressed = true;
    }

    private void OnSpaceSkill(InputAction.CallbackContext ctx)
    {
        if (!isSkill)
        {
            SpaceSkillPressed = true;
            skillCode = playerStat.spaceSkill.code;
            skillDamage = playerStat.spaceSkill.damage;
        }
    }

    private void OnCSkill(InputAction.CallbackContext ctx)
    {
        if (!isSkill)
        {
            CSkillPressed = true;
            skillCode = playerStat.cSkill.code;
            skillDamage = playerStat.cSkill.damage;
        }
    }

    private void OnRSkill(InputAction.CallbackContext ctx)
    {
        if (!isSkill)
        {
            RSkillPressed = true;
            skillCode = playerStat.rSkill.code;
            skillDamage = playerStat.rSkill.damage;
        }
    }

    private void OnGuard(InputAction.CallbackContext ctx)
    {
        GuardPressed = ctx.ReadValue<float>() > 0f;
    }

    private void OnEnteraction(InputAction.CallbackContext ctx)
    {
        EnteractionPressed = ctx.ReadValue<float>() > 0f;
    }

    // Coroutine
    public void StartCoroutinePlayer(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }


    public PlayerAnimator PlayerAnimator => playerAnimator;
    public Animator Animator => animator;
    public HitColor PlayerHitColor => playerHitColor;
    public PlayerGuardTrail GuardTrail => _guardTrail;
    public PlayerAttackTrail AttackTrail => _attackTrail;
    public SwordHitbox AttackHitbox => _attackHitbox;
}

