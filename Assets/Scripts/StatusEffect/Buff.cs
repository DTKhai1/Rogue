using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string buffName;
    public float duration;
    public Sprite icon;
    public int price;
    public abstract void ApplyBuff(GameObject target);
}