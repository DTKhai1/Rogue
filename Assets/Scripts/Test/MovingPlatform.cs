using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IFixedUpdateObserver
{
    public float speed;
    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    public void ObservedFixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + speed*Time.deltaTime, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LimitCol"))
        {
            FlipDirection();
        }
        if (collision.gameObject.TryGetComponent(out Damageable dmg))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damageable dmg))
        {
            collision.transform.SetParent(null);
        }
    }
    private void FlipDirection()
    {
        speed *= -1;
    }
}
