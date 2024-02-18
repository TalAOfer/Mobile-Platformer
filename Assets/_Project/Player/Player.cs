using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public int FacingDirection { get; private set; }
    public bool IsGrounded { get; private set; }
    
    #region Components
    public Rigidbody2D RB { get; private set; }
    public Animator Anim;
    public PlayerInputHandler playerInput { get; private set; }
    [SerializeField] private InputActionAsset inputAsset;
    
    #endregion

    #region State Machine Variables
    public FiniteStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerInAirState InAirState { get; private set; }

    public PlayerLandState LandState { get; private set; }

    public PlayerDeathState DeathState { get; private set; }

    #endregion

    #region Check Transforms

    [SerializeField] private Transform groundCheck;

    #endregion

    #region Other Variables 

    private Vector2 workSpace;
    public Vector2 CurrentVelocity { get; private set; }

    #endregion


    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new FiniteStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "die");
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInputHandler>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        IsGrounded = CheckIfGrounded();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity, angle.y * velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
        
    #endregion

    #region Check Functions

    public bool CheckIfGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }


    #endregion

    #region Other Functions

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void DisableInput()
    {
        
        inputAsset.Disable();
    }

    public void Die()
    {
        StateMachine.ChangeState(DeathState);
    }

    public void Teleport(Vector3 position, float force, Vector2 direction)
    {
        transform.position = position;
        SetVelocity(force, direction);        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }

    #endregion
}
