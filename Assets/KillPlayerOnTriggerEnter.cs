using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
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
