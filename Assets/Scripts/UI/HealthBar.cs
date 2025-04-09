
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthText;

    Player playerDamageable;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Player>();
        healthSlider.value = SliderValueCalculator(playerDamageable.Health, playerDamageable.MaxHealth);
        healthText.text = playerDamageable.Health.ToString();
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }


    private float SliderValueCalculator(float health, float maxHealth)
    {
        return health/ maxHealth;
    }

    public void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthText.text = newHealth.ToString();
        healthSlider.value = SliderValueCalculator(playerDamageable.Health, playerDamageable.MaxHealth);
    }
}
