using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawning : MonoBehaviour
{
    public List<GameObject> PlayerCharacters = new List<GameObject>();

    private void Awake()
    {
        Instantiate(PlayerCharacters[0], transform.position, Quaternion.identity);
    }
}
