using UnityEngine;

[CreateAssetMenu(fileName = "New Ignite Effect", menuName = "Buffs/Effects/Ignite")]
public class IgniteEffect : EffectBuff
{
    public int igniteDamage;
    public float tickInterval;

    public override void ApplyBuff(GameObject target)
    {
        var debuffManager = target.GetComponent<StatusEffectManager>();
        if (debuffManager != null)
        {
            bool hasIgnite = false;
            foreach(var debuff in debuffManager.ActiveDebuffs)
            {
                if(debuff is IgniteDebuff igniteDebuff)
                {
                    hasIgnite = true;
                    igniteDebuff.remainDuration = igniteDebuff.duration;
                }
            }
            if (!hasIgnite)
            {
                var igniteDebuff = CreateInstance<IgniteDebuff>();
                igniteDebuff.igniteDamage = igniteDamage;
                igniteDebuff.tickInterval = tickInterval;
                igniteDebuff.duration = duration;
                debuffManager.AddDebuff(igniteDebuff);
            }
        }
    }
}