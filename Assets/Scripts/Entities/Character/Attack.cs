using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;
    public Vector2 knockback;
    public PlayerStats stats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0? knockback : new Vector2(-knockback.x, knockback.y);

            damageable.Hit((int)(attackDamage * stats.DamageMultiplier), deliveredKnockback);
            if(transform.parent.TryGetComponent(out Damageable attackerDamageable))
            {
                attackerDamageable.OnAttackEffects.Invoke(collision.gameObject);
            }
        }
    }
}
