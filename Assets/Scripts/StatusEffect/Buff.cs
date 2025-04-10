using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string buffName;
    public float duration;
    public Sprite icon;
    public int price;
    public string description;
    public abstract void ApplyBuff(GameObject target);
}