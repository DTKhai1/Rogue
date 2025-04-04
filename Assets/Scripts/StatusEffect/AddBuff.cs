using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuff : MonoBehaviour, IInteractable
{
    public BuffList buffList;
    public Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Interact()
    {
        var poisonBuff = ScriptableObject.CreateInstance<IgniteEffect>();
        poisonBuff.igniteDamage = 5;
        poisonBuff.tickInterval = .5f;
        poisonBuff.duration = 5f;
        player.AddBuff(poisonBuff);
        player.AddAttackEffect(poisonBuff);
        var strength = ScriptableObject.CreateInstance<StrengthBuff>();
        strength.stats = player.stats;
        strength.ApplyBuff(player.gameObject);
    }
}
