using Cinemachine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossMapInitializer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _playerSpawner;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _playerHealthUI;
    [SerializeField] private GameObject _victoryUI;
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
        _playerHealthUI = Instantiate(_playerHealthUI);
        _map = Instantiate(_map);
        _boss = Instantiate(_boss);
        _victoryUI = Instantiate(_victoryUI);
    }

    private async Task LoadObject()
    {
        _mainCamera = Instantiate(_mainCamera);
        _eventSystem = Instantiate(_eventSystem);
    }

    private void PrepareGame()
    {
        _victoryUI.SetActive(false);
        gameManager.ChangeState(GameState.Playing);
        gameManager.audioManager.PlayMusic(gameManager.audioManager.backGroundBoss);
    }
    public void BossDefeatDelay()
    {
        StartCoroutine(OpenVictoryUI());
    }
    private IEnumerator OpenVictoryUI()
    {
        yield return new WaitForSeconds(2f);
        gameManager.ChangeState(GameState.Victory);
    }
}
