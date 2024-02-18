using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    private Player player;
    private bool isCharging;
    private bool canShoot = true;
    private BowLineManager lineHandler;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float spawnDistance = 1;
    [SerializeField] private float shootForce = 2;
    [SerializeField] private GameEvent TeleportPlayer;
    Vector3 temp = Vector3.zero;
    Animator anim;
    public void EnableShooting() => canShoot = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
        lineHandler = GetComponent<BowLineManager>();
    }

    public void Charge()
    {
        anim.Play("Charge");
        lineHandler.EnableDraw(true);
        isCharging = true;
    }

    private void Update()
    {
        if (player.playerInput.ShootInput && !isCharging)
        {
            if (canShoot)
            {
                anim.Play("Charge");
                lineHandler.EnableDraw(true);
                isCharging = true;
            }
        }

        if (!player.playerInput.ShootInput && isCharging)
        {
            isCharging = false;
            canShoot = false;
            lineHandler.EnableDraw(false);
            Shoot();
            anim.Play("Idle");
            transform.eulerAngles = Vector3.zero;
        }

        if (isCharging)
        {
            temp.z = player.playerInput.AimingAngle;
            transform.eulerAngles = temp;
        }
    }

    private void Shoot()
    {
        Vector3 startPosition = transform.position + player.playerInput.AimingDirection * spawnDistance;

        // Instantiate the arrow with the rotation towards the aiming direction
        GameObject arrow = Instantiate(_arrowPrefab, startPosition, transform.rotation);

        // Apply a force to the arrow's Rigidbody2D to shoot it "forward" relative to its own orientation
        if (arrow.TryGetComponent<Rigidbody2D>(out var arrowRb))
        {
            // Since the arrow is already rotated to face the aiming direction, use its right vector to apply the force
            arrowRb.AddForce(arrow.transform.right * shootForce, ForceMode2D.Impulse);
        }
    }


}
