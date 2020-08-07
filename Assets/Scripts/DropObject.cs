using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    PlayerController player;
    StageManager stageManager;
    float centerPositionX;
    float centerPositionY;
    float goalPosition;
    public float moveLeach = 5;
    public float reactionLeachX = 1;
    public float reactionLeachY = 5;
    public float gravitySpeed = 0.5f;


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
        //stageManager.player.rb.gravityScale = 2;
        float nowPositionX = gameObject.transform.position.x;
        float nowPositionY = gameObject.transform.position.y;
        if (stageManager.playerPos.position.x < (centerPositionX + reactionLeachX) && stageManager.playerPos.position.x > (centerPositionX - reactionLeachX))
        {
            if (stageManager.playerPos.position.y < (centerPositionY + reactionLeachY) && stageManager.playerPos.position.y > (centerPositionY - reactionLeachY))
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;                
                gameObject.GetComponent<Rigidbody2D>().gravityScale = gravitySpeed;
            }
        }
        if (nowPositionY < (nowPositionY - 10))
        {
            Destroy(gameObject);
        }
    }
}
