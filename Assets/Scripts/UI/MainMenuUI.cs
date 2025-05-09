using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject mainMenu;
    public GameObject SettingMenu;
    public PlayerStats playerStats;
    public BuffList buffList;

    public Slider musicSlider;
    public Slider SFXSlider;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.audioManager.PlayMusic(gameManager.audioManager.backGroundNorm);
        musicSlider.value = gameManager.audioManager.m_Source.volume;
        SFXSlider.value = gameManager.audioManager.SFXSource.volume;
    }
    public void StartGame()
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        gameManager.levelManager.currentLevel = 0;
        gameManager.levelManager.levelPlayed.Clear();
        playerStats.resetStat();
        buffList.allBuffs.Clear();
        gameManager.gameObject.GetComponent<BuffListDisplay>().DisplayBuffs();
        gameManager.levelManager.ChestLeft = 0;
        gameManager.GoToNextLevel(gameManager.levelManager.CombatMin, gameManager.levelManager.CombatMax);
    }

    public void OpenSetting()
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        mainMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }

    public void QuitSetting()
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        mainMenu?.SetActive(true);
        SettingMenu?.SetActive(false);
    }
    public void ExitGame()
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        Application.Quit();
    }
    public void OnMusicVolumeChanged()
    {
        gameManager.audioManager.m_Source.volume = musicSlider.value;
        UpdateUI();
    }
    public void OnSFXVolumeChanged()
    {
        gameManager.audioManager.SFXSource.volume = SFXSlider.value;
        UpdateUI();
    }
    public void UpdateUI()
    {
        gameManager.audioManager.musicSlider.value = gameManager.audioManager.m_Source.volume;
        gameManager.audioManager.SFXSlider.value = gameManager.audioManager.SFXSource.volume;
    }
}
