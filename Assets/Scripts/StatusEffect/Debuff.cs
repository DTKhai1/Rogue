using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Debuff : ScriptableObject
{
    public string debuffName;
    public Sprite icon;
    public float remainDuration;
    public bool isEffectActive;

    protected GameObject target;

    public virtual void ApplyDebuff(GameObject target)
    {
    }
    public virtual void UpdateCall()
    {

    }

    public virtual void UpdateEffect()
    {

    }

    public virtual void RemoveEffct()
    {
        remainDuration = 0;
        isEffectActive = false;
    }
}