using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _playerSpawner;
    [SerializeField] private GameObject _enemySpawners;
    [SerializeField] private GameObject _combatPortal;
    [SerializeField] private GameObject _occurPortal;
    [SerializeField] private GameObject _shopPortal;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject _chest;
    [SerializeField] private PlayerStats _playerStats;
    private GameManager gameManager;
    private async void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        await LoadObject();
        await CreateObjects();
        PrepareGame();
    }

    private async Task CreateObjects()
    {
        _playerSpawner = Instantiate(_playerSpawner);
        vCam = Instantiate(vCam);
        UI = Instantiate(UI);
        _map = Instantiate(_map);
        _enemySpawners = Instantiate(_enemySpawners);
        _combatPortal = Instantiate(_combatPortal);
        _occurPortal = Instantiate(_occurPortal);
        _shopPortal = Instantiate(_shopPortal);
        _chest = Instantiate(_chest);
    }

    private async Task LoadObject()
    {
        _mainCamera = Instantiate(_mainCamera);
        _eventSystem = Instantiate(_eventSystem);
    }

    private void PrepareGame()
    {
        _chest.SetActive(false);
        _combatPortal.SetActive(false);
        _occurPortal.SetActive(false);
        _shopPortal.SetActive(false);
        gameManager.ChangeState(GameState.Playing);
    }

    public void OpenPortal()
    {
        if(gameManager.levelManager.currentLevel == 3)
        {
            _shopPortal.SetActive(true);
        }
        else
        {
            _combatPortal.SetActive(true);
            _occurPortal.SetActive(true);
        }
    }

    public void ShowChest()
    {
        _chest.SetActive(true);
    }
} 
