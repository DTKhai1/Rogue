using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class fadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;

    public GameObject heartDrop;
    [NonSerialized] private float heartDropRate = 0.2f;
    [NonSerialized] private float goldDropProbability = 0.5f;

    private GameManager gameManager;
    private GameInitializer gameInitializer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameInitializer = GameObject.FindGameObjectWithTag("GameInitiator").GetComponent<GameInitializer>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;

        float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if(timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
            if (objToRemove.CompareTag("Enemy"))
            {
                float dropGacha = Random.Range(0.0f, 1.0f);
                if (dropGacha <= heartDropRate) Instantiate(heartDrop, objToRemove.transform.position, Quaternion.identity);
                gameManager.enemyManager.enemyCounter--;
                gameManager.playerStats.Gold += (dropGacha < goldDropProbability) ? 2 : 1;
                gameManager.goldUpdate.Invoke(gameManager.playerStats.Gold);
                if (gameManager.enemyManager.enemyCounter == 0 && gameManager.enemyManager.IsSpawnerActive()) gameInitializer.ShowChest();
            }
            if (objToRemove.CompareTag("Player"))
            {
                gameManager.ChangeState(GameState.GameOver);
            }
        }
    }
}
