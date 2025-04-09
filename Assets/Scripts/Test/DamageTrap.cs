using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour, IFixedUpdateObserver
{
    public int damage;
    private bool isOnTrap;
    private Player target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            isOnTrap = true;
            target = player;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnTrap = false;
        target = null;
    }
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
        if(isOnTrap && target != null)
        {
            if(!target.isInvisible)
            {
                target.Hit(damage, Vector2.zero);
            }
        }
    }
}
