using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelType
{
    Combat,
    Occurence,
    Shop,
    Boss
}
public class LevelPortal : MonoBehaviour, IInteractable
{
    private GameManager gameManager;
    public LevelType type;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void Interact()
    {
        switch (type)
        {
            case LevelType.Combat:
                gameManager.GoToNextLevel(gameManager.levelManager.CombatMin, gameManager.levelManager.CombatMax);
                break;
            case LevelType.Occurence:
                gameManager.GoToNextLevel(gameManager.levelManager.OccurMin, gameManager.levelManager.OccurMax);
                break;
            case LevelType.Shop:
                gameManager.GoToNonGachaLevel(gameManager.levelManager.Shop, gameManager.levelManager.Shop);
                break; 
            case LevelType.Boss:
                gameManager.GoToNonGachaLevel(gameManager.levelManager.bossMin, gameManager.levelManager.bossMax);
                break;

        }
    }
}
