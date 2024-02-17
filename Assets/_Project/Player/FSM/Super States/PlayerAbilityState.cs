using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    public PlayerAbilityState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            } 
            
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
             
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
