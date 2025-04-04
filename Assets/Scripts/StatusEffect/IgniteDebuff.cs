using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ignite Debuff", menuName = "Debuffs/Ignite")]
public class IgniteDebuff : Debuff
{
    public int igniteDamage;
    public float tickInterval;
    public float duration;
    [SerializeField] private float tickIntervalCD;

    public override void ApplyDebuff(GameObject target)
    {
        base.ApplyDebuff(target);
        this.target = target;
        remainDuration = duration;
        isEffectActive = true;
    }
    public override void UpdateEffect()
    {
        base.UpdateEffect();
        if (isEffectActive)
        {
            if (target.TryGetComponent(out Damageable damageable))
            {
                damageable.EffectApplyDamage(igniteDamage);
            }
        }
    }
    public override void UpdateCall()
    {
        base.UpdateCall();
        if (isEffectActive)
        {
            remainDuration -= Time.deltaTime;
            if (remainDuration <= 0)
            {
                isEffectActive = false;
            }
        }


        tickIntervalCD += Time.deltaTime;
        if (tickIntervalCD >= tickInterval)
        {
            UpdateEffect();
            tickIntervalCD = 0;
        }
    }

}