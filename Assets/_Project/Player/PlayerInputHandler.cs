using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float JumpInputStartTime;

    public Vector3 AimingDirection { get; private set; }
    public float AimingAngle { get; private set; }

    public bool ShootInput { get; private set; }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CalculateAimingDirection();
        CalculateAimingAngle();
    }

    private void CalculateAimingDirection()
    {
        Vector3 start = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Adjust for 2D setup
        AimingDirection = (mousePosition - start).normalized;
    }

    private void CalculateAimingAngle()
    {
        float aimingAngle = Mathf.Atan2(AimingDirection.y, AimingDirection.x) * Mathf.Rad2Deg;

        if (playerData.shouldSnapAim)
        {
            float divisionSize = 360f / playerData.angleDevisor; // Calculate size of each division
            aimingAngle = Mathf.Round(aimingAngle / divisionSize) * divisionSize;
        }

        AimingAngle = aimingAngle;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            JumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShootInput = true;
        }

        if (context.canceled)
        {
            ShootInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void CheckJumpInputHoldTime()
    {
        if (Time.time >= JumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
