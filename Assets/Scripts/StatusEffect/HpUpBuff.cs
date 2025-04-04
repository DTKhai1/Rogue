
using UnityEngine;

[CreateAssetMenu(fileName = "HP up", menuName = "Buffs/Effects/HpUp")]
public class HpUpBuff : StatBuff
{
    private void OnEnable()
    {
        type = StatBuffType.HP_up;
    }
}
