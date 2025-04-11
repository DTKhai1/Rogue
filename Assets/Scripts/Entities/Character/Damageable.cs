using UnityEngine;
using UnityEngine.Events;

public interface Damageable
{
    public UnityEvent<GameObject> OnAttackEffects {  get; set; }
    public int Health { get; set; }
    public int MaxHealth { get;}
    public bool IsAlive { get; set; }
    public bool LockVelocity { get; set; }
    public void Hit(int damage, Vector2 knockback);

    public void Heal(int healAmount);
    public void EffectApplyDamage(int damage);
}
