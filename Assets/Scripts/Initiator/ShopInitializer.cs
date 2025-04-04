using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopInitializer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _playerSpawner;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject UI;
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
    }

    private async Task LoadObject()
    {
        _mainCamera = Instantiate(_mainCamera);
        _eventSystem = Instantiate(_eventSystem);
    }

    private void PrepareGame()
    {
        gameManager.ChangeState(GameState.Playing);
    }
}
