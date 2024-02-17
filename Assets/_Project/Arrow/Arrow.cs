using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameEvent OnArrowHitFloor;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        OnArrowHitFloor.Raise(rb, transform.position);
    }

    public void SendEventToPlayer()
    {
        OnArrowHitFloor.Raise(rb, transform.position);
    }
}
