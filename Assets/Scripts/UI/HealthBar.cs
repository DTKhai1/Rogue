
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthText;
    public TMP_Text goldQuantity;

    Player playerDamageable;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerDamageable = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthSlider.value = SliderValueCalculator(playerDamageable.Health, playerDamageable.MaxHealth);
        healthText.text = playerDamageable.Health.ToString();
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
        UpdateGold(gameManager.playerStats.Gold);
        gameManager.goldUpdate.AddListener(UpdateGold);
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
    public void UpdateGold(int gold)
    {
        goldQuantity.text = gold.ToString();
    }
}
