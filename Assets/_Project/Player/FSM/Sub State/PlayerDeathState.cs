using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private bool once;
    public PlayerDeathState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        once = false;
        player.DisableInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        bool isGrounded = player.CheckIfGrounded();
        if (isGrounded && player.CurrentVelocity != (Vector2.zero))
        {
            player.SetVelocityX(0);
        }

        if (!once && Time.time > startTime + playerData.deathDuration)
        {
            once = true;
            playerData.events.Events["RestartScene"].Raise();
        }
    }
}
