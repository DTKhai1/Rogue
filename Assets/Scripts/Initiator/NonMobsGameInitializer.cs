using Cinemachine;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonMobsGameInitializer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _playerSpawner;
    [SerializeField] private GameObject _combatPortal;
    [SerializeField] private GameObject _occurPortal;
    [SerializeField] private GameObject _shopPortal;
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
        _combatPortal = Instantiate(_combatPortal);
        _occurPortal = Instantiate(_occurPortal);
        _shopPortal = Instantiate(_shopPortal);
    }

    private async Task LoadObject()
    {
        _mainCamera = Instantiate(_mainCamera);
        _eventSystem = Instantiate(_eventSystem);
    }

    private void PrepareGame()
    {
        _combatPortal.SetActive(false);
        _occurPortal.SetActive(false);
        _shopPortal.SetActive(false);
        gameManager.ChangeState(GameState.Playing);
    }

    public void OpenPortal()
    {
        if (gameManager.levelManager.currentLevel == 3)
        {
            _shopPortal.SetActive(true);
        }
        else
        {
            _combatPortal.SetActive(true);
            _occurPortal.SetActive(true);
        }
    }
}
