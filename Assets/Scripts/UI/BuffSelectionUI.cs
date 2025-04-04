using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuffSelectionUI : MonoBehaviour
{
    public BuffList buffList;
    public List<Buff> availableBuffs, currentOptions;
    public Player player;

    public Button[] buffSelectButton;
    public Image[] buffSelectImage;
    public TMP_Text[] buffSelectText;

    private GameManager gameManager;
    private GameObject gameInitiator;
    AudioManager audio;
    private void Awake()
    {
        availableBuffs = new List<Buff>(buffList.allBuffs);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameInitiator = GameObject.FindGameObjectWithTag("GameInitiator");
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            buffSelectButton[i].onClick.AddListener(() => SelectBuff(index));
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
            buffSelectText[i].text = currentOptions[i].buffName;
        }
    }

    private void SelectBuff(int index)
    {
        audio.PlaySFX(audio.onClick);
        if (index >= 0 && index < currentOptions.Count)
        {
            player.AddBuff(currentOptions[index]);
            if(currentOptions[index] is EffectBuff effectBuff)
            {
                player.AddAttackEffect(effectBuff);
            }else if (currentOptions[index] is StatBuff statBuff)
            {
                statBuff.stats = player.stats;
                statBuff.ApplyBuff(player.gameObject);
            }
        }
        gameManager.levelManager.ChestLeft--;
        if(gameManager.levelManager.ChestLeft == 0)
        {
            if(gameInitiator.TryGetComponent(out GameInitializer gameInitializer))
            {
                gameInitializer.OpenPortal();
            }else if(gameInitiator.TryGetComponent(out NonMobsGameInitializer nonMobsGameInitializer))
            {
                nonMobsGameInitializer.OpenPortal();
            }
        }
        gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    // Call this method when level is cleared
    public void ShowBuffSelection()
    {
        GenerateBuffOptions();
        UpdateUI();
    }
}
