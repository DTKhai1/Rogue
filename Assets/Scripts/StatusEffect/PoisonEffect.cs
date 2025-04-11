using UnityEngine;

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
            poisonDebuff.poisonDamage = poisonDamage;
            poisonDebuff.tickInterval = tickInterval;
            poisonDebuff.duration = duration;
            debuffManager.AddDebuff(poisonDebuff);
        }
    }
}