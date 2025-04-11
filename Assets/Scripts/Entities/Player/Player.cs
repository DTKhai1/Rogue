using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, Damageable, IUpdateObserver
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    [SerializeField] private UnityEvent<GameObject> _onAttackEffects = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnAttackEffects { get
        {
            return _onAttackEffects;
        }
        set 
        {
            _onAttackEffects = value;
        }
    }



    public PlayerStats stats;
    public BuffList buffList;
    Animator animator;
    private int _maxHealth;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public int Health
    {
        get
        {
            return stats.CurrentHealth;
        }
        set
        {
            stats.CurrentHealth = value;
            
            healthChanged?.Invoke(stats.CurrentHealth, MaxHealth);
            if (stats.CurrentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool isInvisible = false;

    private float timeSinceHit = 0;
    public float invisibiltyTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MaxHealth = stats.CurrentMaxHealth;
        Health = stats.CurrentHealth;
        foreach (var buff in buffList.allBuffs)
        {
            if (buff is EffectBuff effectBuff)
            {
                AddAttackEffect(effectBuff);
            }
        }

    }
    private void OnEnable()
    {
        UpdateManager.RegisterUpdateObserver(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnregisterUpdateObserver(this);
    }

    public void ObservedUpdate()
    {
        if (isInvisible)
        {
            if (timeSinceHit > invisibiltyTime)
            {
                isInvisible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }

    public void UpdateStat()
    {
        if (MaxHealth != stats.CurrentMaxHealth)
        {
            Health += stats.CurrentMaxHealth - MaxHealth;
            MaxHealth = stats.CurrentMaxHealth;
            healthChanged?.Invoke(stats.CurrentHealth, MaxHealth);
        }
    }

    public void Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvisible)
        {
            Health -= damage;
            isInvisible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
        }
    }

    public void Heal(int healAmount)
    {
        if (IsAlive)
        {
            int actualHeal = Mathf.Min(MaxHealth - Health, healAmount);
            Health += actualHeal;

        }
    }

    public void EffectApplyDamage(int damage)
    {
        Health -= damage;
    }
    public void AddBuff(Buff buff)
    {
        buffList.AddBuff(buff);
    }
    public void AddAttackEffect(EffectBuff newEffectBuff)
    {
        OnAttackEffects.AddListener(newEffectBuff.ApplyBuff);
    }
}
