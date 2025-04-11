using UnityEngine;

[CreateAssetMenu(fileName = "Strength Buff", menuName = "Buffs/Effects/Strength")]
public class StrengthBuff : StatBuff
{
    private void OnEnable()
    {
        type = StatBuffType.Strength;
    }
}
