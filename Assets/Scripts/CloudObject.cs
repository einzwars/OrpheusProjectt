using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CloudObject : MonoBehaviour
{
    public float movingSpeed = -1.0f;
    public float setPosition = 10f;
    StageManager stageManager;
    Transform cloudPos;

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        cloudPos = GameObject.Find("5StageClouds").transform;
    }

    void Update()
    {
        gameObject.transform.Translate(movingSpeed, 0, 0);
        if (stageManager.player.checkTrigger == true)
        {
            this.gameObject.transform.position = new Vector3(stageManager.player.savePoint.x + setPosition, cloudPos.position.y, cloudPos.position.z);
        }
    }
}
