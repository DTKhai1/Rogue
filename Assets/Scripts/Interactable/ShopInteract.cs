using UnityEngine;

public class ShopInteract : MonoBehaviour, IInteractable
{
    GameManager gameManager;
    public GameObject shopMenuUI;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        shopMenuUI.SetActive(false);
    }
    public void Interact()
    {
        Time.timeScale = 0;
        gameManager.PlayingUI.SetActive(false);
        shopMenuUI.SetActive(true);
    }

    public void OnClose()
    {
        Time.timeScale = 1;
        shopMenuUI.SetActive(false);
        gameManager.PlayingUI.SetActive(true);
    }
}
