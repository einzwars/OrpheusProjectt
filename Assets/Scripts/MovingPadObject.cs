using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPadObject : MonoBehaviour
{
    public PlayerController player;
    public StageManager stageManager;
    public bool playerIn;
    public bool change;
    public float centerPositionX;
    public float centerPositionY;
    public float goalPosition;
    public float moveLeach = 5;
    public float reactionLeach = 5;
    public float movingSpeed = 0.1f;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        goalPosition = centerPositionX + moveLeach;
    }

    // Update is called once per frame
    void Update()
    {        
        float nowPosition = gameObject.transform.position.x;            
        if(stageManager.playerPos.position.x < (centerPositionX + reactionLeach) && stageManager.playerPos.position.x > (centerPositionX - reactionLeach))
        {
            if (change == false)
            {
                gameObject.transform.Translate(movingSpeed, 0, 0);                
                if(playerIn == true)
                {                    
                    player.gameObject.transform.Translate(movingSpeed, 0, 0);
                }
                
            }
            else if (change == true)
            {
                gameObject.transform.Translate(-movingSpeed, 0, 0);
                if (playerIn == true)
                {
                    player.gameObject.transform.Translate(-movingSpeed, 0, 0);
                }
            }
            if (nowPosition > goalPosition)
            {
                change = true;
            }
            if (nowPosition < centerPositionX)
            {
                change = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
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


}
