using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private BuffListDisplay buffListDisplay;
    private GameObject gameInitiator;
    private void Awake()
    {
        availableBuffs = new List<Buff>(buffList.allBuffs);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buffListDisplay = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuffListDisplay>();
        gameInitiator = GameObject.FindGameObjectWithTag("GameInitiator");
    }

    private void OnEnable()
    {
        ShowBuffSelection();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < buffSelectButton.Length; i++)
        {
            int index = i; 
            buffSelectButton[i].onClick.AddListener(() => SelectBuff(index));
        }
    }

    private void GenerateBuffOptions()
    {
        currentOptions.Clear();

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
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
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
            buffListDisplay.DisplayBuffs();
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
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void ShowBuffSelection()
    {
        GenerateBuffOptions();
        UpdateUI();
    }
}
