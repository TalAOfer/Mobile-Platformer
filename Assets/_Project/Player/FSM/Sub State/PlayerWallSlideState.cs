using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    private int xInput;
    private bool jumpInput;
    private int yInput;
    private bool isGrounded;
    private bool isTouchingWall;

    public PlayerWallSlideState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityY(-playerData.wallSlideVelocity);

        xInput = player.playerInput.NormInputX;
        yInput = player.playerInput.NormInputY;
        jumpInput = player.playerInput.JumpInput;

        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        else if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(xInput);
            stateMachine.ChangeState(player.WallJumpState);
        }

        else if (!isTouchingWall || yInput < 0)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
}
