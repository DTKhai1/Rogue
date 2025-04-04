using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoss : MonoBehaviour
{
    public GameObject spell;

    private bool facingLeft;
    private Transform player;

    private void Start()
    {
        facingLeft = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void LookAtPlayer(Transform player)
    {
        if(player.position.x < transform.position.x && !facingLeft)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            facingLeft = true;
        }else if(player.position.x > transform.position.x && facingLeft)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            facingLeft = false;
        }
    }
    public void castSpell()
    {
        Instantiate(spell, new Vector2(player.position.x, player.position.y + 2), Quaternion.identity);
    }
}
