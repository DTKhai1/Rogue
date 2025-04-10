using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuUI : MonoBehaviour
{
    public BuffList buffList;
    public List<Buff> availableBuffs, currentOptions;
    public Player player;

    public Button[] buffSelectButton;
    public Image[] buffSelectImage;
    public TMP_Text[] buffSelectText;

    GameManager gameManager;
    private BuffListDisplay buffListDisplay;
    private void Awake()
    {
        availableBuffs = new List<Buff>(buffList.allBuffs);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buffListDisplay = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuffListDisplay>();
    }

    private void OnEnable()
    {
        ShowBuffSelection();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // Add click listeners to buttons
        for (int i = 0; i < buffSelectButton.Length; i++)
        {
            int index = i; // Needed for closure
            buffSelectButton[i].onClick.AddListener(() => { SelectBuff(index);}
            );
        }
    }

    private void GenerateBuffOptions()
    {
        currentOptions.Clear();

        // Get 3 random unique buffs
        for (int i = 0; i < 3 && availableBuffs.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableBuffs.Count);
            currentOptions.Add(availableBuffs[randomIndex]);
            availableBuffs.RemoveAt(randomIndex);
        }
    }
    private void UpdateUI()
    {
        for (int i = 0; i < currentOptions.Count; i++)
        {
            buffSelectImage[i].sprite = currentOptions[i].icon;
            buffSelectText[i].text = currentOptions[i].price.ToString();
        }
    }

    private void SelectBuff(int index)
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        if (index >= 0 && index < currentOptions.Count)
        {
            if (gameManager.playerStats.Gold >= currentOptions[index].price)
            {
                player.AddBuff(currentOptions[index]);
                gameManager.playerStats.Gold -= currentOptions[index].price;
                gameManager.goldUpdate.Invoke(gameManager.playerStats.Gold);
                if (currentOptions[index] is EffectBuff effectBuff)
                {
                    player.AddAttackEffect(effectBuff);
                }
                else if (currentOptions[index] is StatBuff statBuff)
                {
                    statBuff.stats = player.stats;
                    statBuff.ApplyBuff(player.gameObject);
                }
                buffListDisplay.DisplayBuffs();
                buffSelectButton[index].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    // Call this method when level is cleared
    public void ShowBuffSelection()
    {
        GenerateBuffOptions();
        UpdateUI();
    }
}
