using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
    PlayerController player;
    StageManager stageManager;
    public float centerPositionX;
    public float centerPositionY;
    public float nowPositionX;
    public float nowPositionY;    
    public float moveLeach = 5;
    public float reactionLeachX = 5;
    public float reactionLeachY = 1;    
    public float movingSpeed = 0.01f;
    public bool right = false;
    public bool left = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;        
    }
    
    void Update()
    {
        float nowPositionX = gameObject.transform.position.x;
        float nowPositionY = gameObject.transform.position.y;        
        if (right == true)
        {
            gameObject.transform.Translate(0, movingSpeed, 0);
            if (nowPositionX > (centerPositionY + 1))
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.GetComponent<Renderer>().enabled = false;
                left = false;
                right = false;
            }
        }
        if (left == true)
        {
            gameObject.transform.Translate(0, movingSpeed, 0);
            if (nowPositionX > (centerPositionY - 1))
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.GetComponent<Renderer>().enabled = false;
                left = false;
                right = false;
            }
        }
        if (stageManager.playerPos.position.x < (centerPositionX) && stageManager.playerPos.position.x > (centerPositionX - reactionLeachX))
        {
            if (stageManager.playerPos.position.y < (centerPositionY + reactionLeachY) && stageManager.playerPos.position.y > (centerPositionY - reactionLeachY))
            {
                left = true;
            }
        }
        if (stageManager.playerPos.position.x < (centerPositionX + reactionLeachX) && stageManager.playerPos.position.x > centerPositionX)
        {
            if (stageManager.playerPos.position.y < (centerPositionY + reactionLeachY) && stageManager.playerPos.position.y > (centerPositionY - reactionLeachY))
            {
                right = true;
            }
        }
    }
}
