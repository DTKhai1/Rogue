using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour
{
    public GameObject chest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        chest.SetActive(true);
    }
}
