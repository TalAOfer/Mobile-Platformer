using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.Die();
            }
        }
    }
}
