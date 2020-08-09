using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPadObject : MonoBehaviour
{
    PlayerController player;
    StageManager stageManager;
    bool playerIn;
    bool change;
    float centerPositionX;
    float centerPositionY;
    float goalPosition;
    public float moveLeach = 5;
    public float reactionLeach = 5;
    public float movingSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        goalPosition = centerPositionY + moveLeach;
    }

    // Update is called once per frame
    void Update()
    {        
        float nowPositionX = gameObject.transform.position.x;
        float nowPositionY = gameObject.transform.position.y;        
        if (stageManager.playerPos.position.x < (centerPositionX + reactionLeach) && stageManager.playerPos.position.x > (centerPositionX - reactionLeach))
        {
            if (change == false)
            {
                gameObject.transform.Translate(0, movingSpeed, 0);
                if (playerIn == true)
                {
                    player.gameObject.transform.Translate(0, movingSpeed, 0);
                }    
            }
            else if (change == true)
            {
                gameObject.transform.Translate(0, -movingSpeed, 0);
                if (playerIn == true)
                {
                    player.gameObject.transform.Translate(0, -movingSpeed, 0);
                }
            }
            if (nowPositionY > goalPosition)
            {
                change = true;
            }
            if (nowPositionY < centerPositionY)
            {
                change = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            change = false;
        }
    }
}
