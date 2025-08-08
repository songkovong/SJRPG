using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    private BaseState currentState;

    public PlayerInput playerInput;
    public CharacterController characterController { get; private set; }
    PlayerAnimator playerAnimator;
    Animator animator;


    HitColor playerHitColor;
    PlayerGuardTrail _guardTrail;

    // Effects and Hitbox
    public SwordHitbox swordHitbox { get; private set; }
    public PlayerTrail swordTrail { get; private set; }

    // Player Stat
    public PlayerStat playerStat { get; private set; }

    // Player Inventory
    public Inventory inventory { get; private set; }


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
    public bool EnteractionPressed { get; set; }
    public bool PickupPressed { get; set; }
    public bool PausePressed { get; set; }

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
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);
        characterController = GetComponent<CharacterController>();

        playerStat = GetComponent<PlayerStat>();

        playerHitColor = GetComponent<HitColor>();
        _guardTrail = GetComponentInChildren<PlayerGuardTrail>();

        inventory = GetComponent<Inventory>();


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
        playerInput.Player.Enteraction.performed += OnEnteraction;
        playerInput.Player.PickUp.performed += OnPickUp;
        playerInput.Player.Num1.performed += OnNum1;
        playerInput.Player.Num2.performed += OnNum2;
        playerInput.Player.Num3.performed += OnNum3;
        playerInput.Player.Num4.performed += OnNum4;
        playerInput.Player.Pause.performed += OnPause;
        

        isHit = false;

        playerInput.Enable();
        // playerInput.Player.Disable();
    }


    void Start()
    {
        ChangeState(new MoveState(this));
        inventory.AcquireCoin(100000);
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

        // if (GameManager.instance.isPaused)
        // {
        //     playerInput.Player.Disable();
        //     DontRotate = true;
        // }
        // else
        // {
        //     playerInput.Player.Enable();
        //     DontRotate = false;
        // }

        Debug.Log("Skill Code = " + skillCode);

        // if (DialogueManager.Instance.IsPlaying())
        // {
        //     playerInput.Player.Disable();
        // }
        // else
        // {
        //     playerInput.Player.Enable();
        // }
    }

    #region Methods
    public void PlayerMove(float multipleSpeed)
    {
        // Little Gravity on Player
        currentMovement.y = -1f;

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
        if (!DontRotate)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

            // Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            // if (groundPlane.Raycast(ray, out float distance))
            // {
            //     Vector3 direction = ray.GetPoint(distance) - transform.position;
            //     direction.y = 0f;

            //     // If vector is zero, dont rotate
            //     if (direction.sqrMagnitude < 0.001f)
            //         return;

            //     Quaternion targetRotation = Quaternion.LookRotation(direction);
            //     this.transform.rotation = Quaternion.Slerp(
            //         this.transform.rotation,
            //         targetRotation,
            //         Time.deltaTime * (SprintPressed ? playerStat.data.rotationSpeed : playerStat.data.rotationSpeed / 2f)
            //     );
            // }
            RaycastHit hit;

            int layerMask = ~(1 << LayerMask.NameToLayer("IgnoreWall"));

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.CompareTag("Camera Target") || hit.collider.CompareTag("NPC") || hit.collider.CompareTag("Enemy"))
                {
                    Vector3 direction = hit.point - transform.position;
                    direction.y = 0f;

                    // If vector is zero, dont rotate
                    if (direction.sqrMagnitude < 0.001f)
                        return;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    this.transform.rotation = Quaternion.Slerp(
                        this.transform.rotation,
                        targetRotation,
                        Time.deltaTime * (SprintPressed ? playerStat.data.rotationSpeed : playerStat.data.rotationSpeed / 2f)
                    );
                }
            }
        }
    }

    public void LocalMoveDir()
    {
        localMovement = this.transform.InverseTransformDirection(new Vector3(currentMovement.x, 0, currentMovement.z));
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void SavePlayerPosAndRot()
    {
        Vector3 pos = gameObject.transform.position;
        Quaternion rot = gameObject.transform.rotation;

        PlayerPrefs.SetFloat("PlayerPosX", pos.x);
        PlayerPrefs.SetFloat("PlayerPosY", pos.y);
        PlayerPrefs.SetFloat("PlayerPosZ", pos.z);
        PlayerPrefs.SetFloat("PlayerRotX", rot.x);
        PlayerPrefs.SetFloat("PlayerRotY", rot.y);
        PlayerPrefs.SetFloat("PlayerRotZ", rot.z);
        PlayerPrefs.SetFloat("PlayerRotW", rot.w);

        PlayerPrefs.Save();
    }

    public void LoadPlayerPosAndRot()
    {
        if (!PlayerPrefs.HasKey("PlayerPosX"))
        {
            return;
        }
        else
        {
            float px = PlayerPrefs.GetFloat("PlayerPosX");
            float py = PlayerPrefs.GetFloat("PlayerPosY");
            float pz = PlayerPrefs.GetFloat("PlayerPosZ");

            float rx = PlayerPrefs.GetFloat("PlayerRotX");
            float ry = PlayerPrefs.GetFloat("PlayerRotY");
            float rz = PlayerPrefs.GetFloat("PlayerRotZ");
            float rw = PlayerPrefs.GetFloat("PlayerRotW");

            characterController.enabled = false;

            gameObject.transform.SetPositionAndRotation(new Vector3(px, py, pz), new Quaternion(rx, ry, rz, rw));

            characterController.enabled = true;
        }
    }

    public void SetWeaponHitbox(SwordHitbox hitbox)
    {
        Debug.Log("Sword Hitbox On");
        swordHitbox = hitbox;
        // swordHitbox?.HitboxOn();
        // swordHitbox?.HitboxOff();
    }

    public void SetWeaponTrail(PlayerTrail trail)
    {
        Debug.Log("Sword Trail On");
        swordTrail = trail;
        // swordTrail?.StartTrail();
        // swordTrail?.EndTrail();
    }

    #region Animation Event

    public void AttackStart() => swordHitbox?.HitboxOn();
    public void AttackEnd() => swordHitbox?.HitboxOff();
    public void TrailStart() => swordTrail?.StartTrail();
    public void TrailEnd() => swordTrail?.EndTrail();

    #endregion

    #endregion

    #region On Event
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
        EnteractionPressed = true;
    }

    private void OnPickUp(InputAction.CallbackContext ctx)
    {
        PickupPressed = true;
    }

    private void OnNum1(InputAction.CallbackContext ctx)
    {
        inventory.UseSlotItem(1);
    }

    private void OnNum2(InputAction.CallbackContext ctx)
    {
        inventory.UseSlotItem(2);
    }

    private void OnNum3(InputAction.CallbackContext ctx)
    {
        inventory.UseSlotItem(3);
    }

    private void OnNum4(InputAction.CallbackContext ctx)
    {
        inventory.UseSlotItem(4);
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        
    }
    #endregion

    #region Coroutine
    public void StartCoroutinePlayer(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    #endregion


    public PlayerAnimator PlayerAnimator => playerAnimator;
    public Animator Animator => animator;
    public HitColor PlayerHitColor => playerHitColor;
    public PlayerGuardTrail GuardTrail => _guardTrail;
}

