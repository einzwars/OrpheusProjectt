using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostObject : MonoBehaviour
{
    PlayerController player;
    public bool change;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (change)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (player.moveDir < 0)
                {
                    player.maxSpeed = 2.0f;
                }
                if (player.moveDir > 0)
                {
                    player.maxSpeed = 4.0f;
                }
            }
        }
        if (!change)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (player.moveDir > 0)
                {
                    player.maxSpeed = 2.0f;
                }
                if (player.moveDir < 0)
                {
                    player.maxSpeed = 4.0f;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (change)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (player.moveDir < 0)
                {
                    player.maxSpeed = 2.0f;
                }
                if (player.moveDir > 0)
                {
                    player.maxSpeed = 4.0f;
                }
            }
        }
        if (!change)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (player.moveDir > 0)
                {
                    player.maxSpeed = 2.0f;
                }
                if (player.moveDir < 0)
                {
                    player.maxSpeed = 4.0f;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (change)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.maxSpeed = 3.0f;
            }
        }
        if (!change)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.maxSpeed = 3.0f;
            }
        }
    }
}
