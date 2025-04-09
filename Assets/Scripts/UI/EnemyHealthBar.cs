using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour, IUpdateObserver
{
    public Slider healthSlider;

    public GameObject target;
    Enemy damageable;
    public Vector3 offset;

    private void Awake()
    {
        damageable = target.GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = SliderValueCalculator(damageable.Health, damageable.MaxHealth);
    }

    public void ObservedUpdate()
    {
        healthSlider.transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
    }

    private void OnEnable()
    {
        UpdateManager.RegisterUpdateObserver(this);
        damageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterUpdateObserver(this);
        damageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float SliderValueCalculator(float health, float maxHealth)
    {
        return health / maxHealth;
    }

    public void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = SliderValueCalculator(damageable.Health, damageable.MaxHealth);
    }
}
