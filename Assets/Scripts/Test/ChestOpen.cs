using UnityEngine;

public class ChestOpen : MonoBehaviour, IInteractable
{
    private Animator anim;
    public GameObject BuffSelectionUI;
    private void Awake()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.levelManager.ChestLeft++;
        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        anim.SetTrigger("isOpen");
    }
}
