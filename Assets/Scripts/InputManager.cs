using UnityEngine;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    
    private bool isAttacking = false;
    private bool isSprinting = false;
    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot= playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look= GetComponent<PlayerLook>();
        

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Chrouch();
        
        
        onFoot.attack.performed += ctx => StartAttack();
        onFoot.attack.canceled += ctx => StopAttack();
        

        onFoot.Sprint.performed += ctx =>
        {
            isSprinting = true;
            motor.Sprint();
        };

        onFoot.Sprint.canceled += ctx =>
        {
            isSprinting = false;
            motor.StopSprint();
        };

        
    }
    void Start()
    {
        
    }
    private void StartAttack()
    {
        isAttacking = true;
        motor.Attack();
    }

    private void StopAttack()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        if (isAttacking)
        {
            motor.Attack();
        }
        if (isSprinting)
            motor.Sprint();

    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
