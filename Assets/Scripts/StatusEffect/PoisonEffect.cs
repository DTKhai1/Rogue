using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Poison Effect", menuName = "Buffs/Effects/Poison")]
public class PoisonEffect : EffectBuff
{
    public int poisonDamage;
    public float tickInterval;
    public override void ApplyBuff(GameObject target)
    {
        var debuffManager = target.GetComponent<StatusEffectManager>();
        if (debuffManager != null)
        {
            var poisonDebuff = CreateInstance<PoisonDebuff>();
            poisonDebuff.poisonDamage = poisonDamage; // Transfer poisonDamage value
            poisonDebuff.tickInterval = tickInterval; // Transfer tickInterval value
            poisonDebuff.duration = duration;
            debuffManager.AddDebuff(poisonDebuff);
        }
    }
}