using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver,
    Victory
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public GameObject PlayingUI;
    public GameObject PauseUI;
    public GameObject VictoryUI;
    public GameObject GameOverUI;
    public GameState CurrentState {  get; private set; }

    public EnemyManager enemyManager;
    public LevelManager levelManager;
    public AudioManager audioManager;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

            Instance = this;
            DontDestroyOnLoad(this);

        enemyManager.enemyCounter = 0;
        enemyManager.spawnerList.Clear();
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
        levelManager.currentLevel = 0;
    }


    public void GoToNextLevel(int firstMap, int lastMap)
    {
        int nextLevel = Random.Range(firstMap, lastMap+1);
        while(levelManager.HasPlayedLevel(nextLevel))
        {
            nextLevel = Random.Range(firstMap, lastMap);
        }
        levelManager.levelPlayed.Add(nextLevel);
        levelManager.currentLevel++;
        enemyManager.spawnerList.Clear();
        levelManager.ExitLevel(nextLevel);
    }

    public void GoToNonGachaLevel(int firstMap, int lastMap)
    {
        int nextLevel = Random.Range(firstMap, lastMap+1);
        while (levelManager.HasPlayedLevel(nextLevel))
        {
            nextLevel = Random.Range(firstMap, lastMap);
        }
        levelManager.currentLevel++;
        enemyManager.spawnerList.Clear();
        levelManager.ExitLevel(nextLevel);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        HandleStateChange();
    }


    private void HandleStateChange()
    {
        HideAllMenus();

        switch (CurrentState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene(0);
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                PlayingUI.SetActive(true);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                PauseUI.SetActive(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                GameOverUI.SetActive(true);
                break;
            case GameState.Victory:
                Time.timeScale = 0;
                VictoryUI.SetActive(true);
                break;
        }
    }
    private void HideAllMenus()
    {
        if(PlayingUI != null) PlayingUI.SetActive(false);
        if(PauseUI != null) PauseUI.SetActive(false);
        if(GameOverUI != null) GameOverUI.SetActive(false);
        if(VictoryUI != null) VictoryUI.SetActive(false);
    }

    //click events
    public void ChangeToPauseMenu()
    {
        audioManager.PlaySFX(audioManager.onClick);
        ChangeState(GameState.Pause);
    }
    public void ChangeToPlayingUI()
    {
        audioManager.PlaySFX(audioManager.onClick);
        ChangeState(GameState.Playing);
    }
    public void ChangeToMainMenu()
    {
        audioManager.PlaySFX(audioManager.onClick);
        ChangeState(GameState.MainMenu);
    }
    public void ChangeToVictoryUI()
    {
        ChangeState(GameState.Victory);
    }
    public void ChangeToGameOverUI()
    {
        ChangeState(GameState.GameOver);
    }
}
