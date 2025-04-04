using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    PermanentEffect,
    TimedEffect,
    TickEffect
}
public class StatusEffect
{
    public virtual void ApplyEffect(GameObject target)
    {

    }
}
public class PermanentEffect: StatusEffect
{
    [SerializeField] private PlayerStats stats;
    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);
        stats = target.GetComponent<PlayerStats>();
    }
}
public class TimedEffect:StatusEffect
{
    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);
        
    }
    public virtual void UpdateEffect(GameObject target, float tickAmount)
    {

    }
}
