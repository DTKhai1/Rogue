using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damage = collision.GetComponent<Damageable>();
        damage.Heal(healthRestore);
        Destroy(gameObject);
        
    }
}
