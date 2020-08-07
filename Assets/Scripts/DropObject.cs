using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public PlayerController player;
    public bool playerIn;
    public bool change;
    public float centerPositionX;
    public float centerPositionY;
    public float goalPosition;
    public float moveLeach = 5;
    public float reactionLeachX = 1;
    public float reactionLeachY = 5;
    public float timer = 0.0f;            // 낙하 시간 계산용 변수

    // Start is called before the first frame update
    void Start()
    {
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        goalPosition = centerPositionY + moveLeach;
    }

    // Update is called once per frame
    void Update()
    {
        float nowPositionX = gameObject.transform.position.x;
        float nowPositionY = gameObject.transform.position.y;
        float playerPositionX = player.transform.position.x;
        float playerPositionY = player.transform.position.y;

        if (playerPositionX < (centerPositionX + reactionLeachX) && playerPositionX > (centerPositionX - reactionLeachX))
        {
            if (playerPositionY < (centerPositionY + reactionLeachY) && playerPositionY > (centerPositionY - reactionLeachY))
            {
                timer += Time.deltaTime;
                if(timer > 1)
                {
                    gameObject.transform.Translate(0, -0.05f, 0);
                }
                
            }
        }
        if (nowPositionY < (nowPositionY - 10))
        {
            Destroy(gameObject);
        }
    }
}
