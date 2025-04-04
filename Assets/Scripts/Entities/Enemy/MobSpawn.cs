using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawn : MonoBehaviour
{
    private Collider2D spawnCol;

    public List<GameObject> spawnList;
    public int maxAmount, minAmount;
    public int mobsAmount;
    
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        spawnCol = GetComponent<Collider2D>();
        mobsAmount = Random.Range(minAmount, maxAmount+1);
        gameManager.enemyManager.enemyCounter += mobsAmount;
        for (int i = 0; i < mobsAmount; i++)
        {
            Instantiate(spawnList[Random.Range(0, spawnList.Count)],
                new Vector2(
                    Random.Range(spawnCol.bounds.min.x, spawnCol.bounds.max.x)
                , spawnCol.bounds.max.y), quaternion.identity);
        }
    }

}
