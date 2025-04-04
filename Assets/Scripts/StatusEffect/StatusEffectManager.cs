using UnityEngine;
using System.Collections.Generic;

public class StatusEffectManager : MonoBehaviour
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
    private void Update()
    {
        if(damageable.IsAlive)
        if(activeDebuffs.Count > 0)
        {
            foreach(var debuff in  activeDebuffs)
            {
                debuff.UpdateCall();
                if (!debuff.isEffectActive)
                {
                    activeDebuffs.Remove(debuff);
                }
            }
        }
    }
}