
using UnityEngine;
public enum StatBuffType{
    HP_up,
    Strength
}
public class StatBuff : Buff
{
    public PlayerStats stats;
    public StatBuffType type;
    public override void ApplyBuff(GameObject target)
    {
        switch (type)
        {
            case StatBuffType.HP_up:
                stats.healthBonus += 0.2f;
                break;
            case StatBuffType.Strength:
                stats.DamageMultiplier += 0.2f;
                break;
            default:
                Debug.Log("stat buff not found");
                break;
        }
        Player player = target.GetComponent<Player>();
        player.UpdateStat();
    }
}
