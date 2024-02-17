using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool _applyVelocityOnTeleport;
    [ShowIf("_applyVelocityOnTeleport")]
    [SerializeField] private float _force;
    [ShowIf("_applyVelocityOnTeleport")]
    [SerializeField] private Vector2 _direction;
    [SerializeField] private Transform _destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Teleport(_destination.transform.position, _force, _direction);
        }
    }
}
