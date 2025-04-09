using UnityEngine;
using System.Collections.Generic;

public class StatusEffectManager : MonoBehaviour, IFixedUpdateObserver
{
    [SerializeField]private List<Debuff> activeDebuffs = new List<Debuff>();
    Damageable damageable;
    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    public List<Debuff> ActiveDebuffs {  get { return activeDebuffs; }
        set
        {
            activeDebuffs = value;
        }
    }

    public void AddDebuff(Debuff newDebuff)
    {
        activeDebuffs.Add(newDebuff);
        newDebuff.ApplyDebuff(gameObject);
    }

    public void RemoveDebuff(Debuff debuffToRemove)
    {
        activeDebuffs.Remove(debuffToRemove);
    }
    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    public void ObservedFixedUpdate()
    {
        if(damageable.IsAlive)
        if(activeDebuffs.Count > 0)
        {
            for (int i = activeDebuffs.Count - 1; i >= 0; i--)
                {
                    if (activeDebuffs[i] != null)
                    {
                        activeDebuffs[i].UpdateCall();
                        if (!activeDebuffs[i].isEffectActive)
                        {
                            activeDebuffs.Remove(activeDebuffs[i]);
                        }
                    }
                }
        }
    }
}