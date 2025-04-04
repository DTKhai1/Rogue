using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string buffName;
    public float duration;
    public Sprite icon;
    public abstract void ApplyBuff(GameObject target);
}