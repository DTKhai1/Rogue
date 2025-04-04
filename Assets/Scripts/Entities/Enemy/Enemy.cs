using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, Damageable
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    [SerializeField] private UnityEvent<GameObject> _onAttackEffects = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnAttackEffects
    {
        get
        {
            return _onAttackEffects;
        }
        set
        {
            _onAttackEffects = value;
        }
    }

    Animator animator;


    [SerializeField]
    private int baseHealth = 100;
    public int MaxHealth
    {
        get
        {
            return baseHealth;
        }
    }

    [SerializeField]
    private int _health;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
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
        Health = MaxHealth;

    }

    private void Update()
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
        if(IsAlive)
        {
            Health -= damage;
        }
    }

}
