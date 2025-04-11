using System.Collections.Generic;
using UnityEngine;

public class SpawnEnable : MonoBehaviour
{

    public List<GameObject> spawnPoints = new List<GameObject>();
    private void Awake()
    {
        foreach (GameObject point in spawnPoints)
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.enemyManager.spawnerList.Add(point);
            point.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(GameObject point in spawnPoints)
            {
                point.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
