using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool jumpInput;
    private bool isGrounded;

    public PlayerGroundedState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.playerInput.NormInputX;
        jumpInput = player.playerInput.JumpInput;
        
        if (jumpInput && player.JumpState.CanJump())
        {
            player.playerInput.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }

        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
