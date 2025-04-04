using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public GameObject target;
    Enemy damageable;

    private void Awake()
    {
        damageable = target.GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = SliderValueCalculator(damageable.Health, damageable.MaxHealth);
    }

    private void OnEnable()
    {
        damageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
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
