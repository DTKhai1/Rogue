using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject mainMenu;
    public GameObject SettingMenu;
    public PlayerStats playerStats;
    public BuffList buffList;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.audioManager.PlayMusic(gameManager.audioManager.backGroundNorm);
    }
    public void StartGame()
    {
        gameManager.audioManager.PlaySFX(gameManager.audioManager.onClick);
        gameManager.levelManager.levelPlayed.Clear();
        playerStats.resetStat();
        buffList.allBuffs.Clear();
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
}
