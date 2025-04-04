using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "ScriptableObject/UIMangager")]
public class UIManager : ScriptableObject
{
    public GameObject playingUI;
    public GameObject pauseUI;
}

