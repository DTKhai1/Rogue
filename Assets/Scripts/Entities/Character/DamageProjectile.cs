using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{
    public int damage;
    public Vector2 knockback = Vector2.zero;
    public float speed;

    GameObject target;
    Rigidbody2D rb;
    Animator anim;
    AudioManager audioManager; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlaySFX(audioManager.explode);
        anim.SetTrigger("explode");
        rb.velocity = Vector2.zero;
        if (collision.gameObject.CompareTag("Player"))
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                Vector2 deliveredKnockback = transform.position.x < target.transform.position.x ? knockback : new Vector2(-knockback.x, knockback.y);

                damageable.Hit(damage, deliveredKnockback);
            }
        }
    }
}
