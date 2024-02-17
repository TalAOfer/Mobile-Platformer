using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMovingDirection : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        // Check if the arrow is moving
        if (rb.velocity != Vector2.zero)
        {
            // Calculate the angle to rotate the arrow
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            // Apply the rotation to the arrow
            rb.rotation = angle;
        }
    }
}
