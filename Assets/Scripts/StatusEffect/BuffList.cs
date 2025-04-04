using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffList", menuName = "Buffs/BuffList")]
public class BuffList : ScriptableObject
{
    public List<Buff> allBuffs = new List<Buff>();
    public void AddBuff(Buff newBuff)
    {
        allBuffs.Add(newBuff);
    }
}
