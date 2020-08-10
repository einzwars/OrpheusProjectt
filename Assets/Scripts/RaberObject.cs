using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaberObject : MonoBehaviour
{
    PlayerController player;
    StageManager stageManager;    
    public int raberIndex;
    public float centerPositionX;
    public float centerPositionY;
    SpriteRenderer spriteRenderer;
    float reactionLeach = 1f;
    public bool[] raberList;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (stageManager.playerPos.position.x < (centerPositionX + reactionLeach) && stageManager.playerPos.position.x > (centerPositionX - reactionLeach))
            {
                if (stageManager.playerPos.position.y < (centerPositionY + reactionLeach) && stageManager.playerPos.position.y > (centerPositionY - reactionLeach))
                {
                    if(raberList[raberIndex] == false)
                    {                        
                        raberList[raberIndex] = true;
                        spriteRenderer.sprite = Resources.Load("StageObject/crank-down", typeof(Sprite)) as Sprite;
                    }
                    else if (raberList[raberIndex] == true)
                    {                        
                        raberList[raberIndex] = false;
                        spriteRenderer.sprite = Resources.Load("StageObject/crank-up", typeof(Sprite)) as Sprite;
                    }
                }
            }
        }
    }
}
