using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelManager", menuName = "ScriptableObject/LevelManager")]
public class LevelManager: ScriptableObject
{
    public List<int> levelPlayed = new List<int>();
    public int CombatMin, CombatMax;
    public int OccurMin, OccurMax;
    public int Shop;
    public int bossMin, bossMax;
    public int currentLevel;
    public int ChestLeft;

    public void ExitLevel(int nextLevel)
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }

    public bool HasPlayedLevel(int nextLevel)
    {
        foreach( var level in levelPlayed )
        {
            if(level == nextLevel ) return true;
        }
        return false;
    }

}
