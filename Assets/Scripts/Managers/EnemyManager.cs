using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObject/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    public int enemyCounter;

    public List<GameObject> spawnerList = new List<GameObject>();
    public bool IsSpawnerActive()
    {
        if(spawnerList.Count == 0) return false;
        foreach(GameObject spawner in spawnerList)
        {
            if (!spawner.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

}
