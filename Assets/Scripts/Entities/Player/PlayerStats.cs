using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObject/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int gold;
    [SerializeField] private int baseHealth = 150;
    [SerializeField] private int currentHealth;


    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] public float healthBonus = 1;

    public int CurrentMaxHealth
    {
        get { return (int)(baseHealth * healthBonus); }
    }
    public int CurrentHealth { get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Max(value, 0);
        }
    }

    public float DamageMultiplier { get { return damageMultiplier; }
        set
        {
            damageMultiplier = value;
        }    
    }
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
        }
    }
    public void resetStat()
    {
        DamageMultiplier = 1f;
        healthBonus = 1f;
        currentHealth = CurrentMaxHealth;
        gold = 0;
    }
}
