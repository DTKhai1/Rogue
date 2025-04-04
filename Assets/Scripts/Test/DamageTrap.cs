using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
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
    private void Update()
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
