using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName ="Player/Data")]
public class PlayerData : ScriptableObject
{
    [Header("Global Data")]
    public AllEvents events;

    [Header("Aim")]
    public int angleDevisor;
    public bool shouldSnapAim;

    [Header("Check Variables")]
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.45f;
    public float wallCheckDistance = 1f;

    [Header("Move")]
    public float movementSpeed = 5f;

    [Header("Jump")]
    public bool dampJumping = true;
    public float jumpForce = 15f;
    public int amountOfJumps = 1;

    [Header("In Air")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMult = 0.5f;

    [Header("Wall Slide State")]
    public bool canSlide = true;
    public float wallSlideVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
}
