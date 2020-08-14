using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitObject : MonoBehaviour
{
    PlayerController player;
    StageManager stageManager;
    public Text waitText;
    public Image panel;
    public GameObject usingText;
    public int waitIndex;
    float centerPositionX;
    float centerPositionY;
    float reactionLeach = 0.5f;
    public string[] waitList;    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        usingText = GameObject.Find("UsingText");
        waitText = usingText.transform.Find("WaitText").GetComponent<Text>();
        panel = usingText.transform.Find("SignPanel").GetComponent<Image>();
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(stageManager.waitIn)
        {
            if (stageManager.playerPos.position.x < (centerPositionX + reactionLeach) && stageManager.playerPos.position.x > (centerPositionX - reactionLeach))
            {
                if (stageManager.playerPos.position.y < (centerPositionY + reactionLeach) && stageManager.playerPos.position.y > (centerPositionY - reactionLeach))
                {
                    waitText.text = waitList[waitIndex];
                    waitText.gameObject.SetActive(true);  
                    panel.gameObject.SetActive(true);                  
                }
            }                
        }
    }
}
