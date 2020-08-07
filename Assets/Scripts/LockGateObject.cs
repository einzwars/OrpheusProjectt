using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGateObject : MonoBehaviour
{
    public RaberObject raberObject;
    public int lockIndex;
    float centerPositionX;
    float centerPositionY;
    float goalPositionY;
    public float moveLeach = 5;    
    
    // Start is called before the first frame update
    void Start()
    {
        raberObject = GameObject.Find("Raber"+lockIndex.ToString()).GetComponent<RaberObject>();        
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        goalPositionY = centerPositionY + moveLeach;
    }

    // Update is called once per frame
    void Update()
    {
        float nowPositionX = gameObject.transform.position.x;
        float nowPositionY = gameObject.transform.position.y;
        if (raberObject.raberList[lockIndex] == true)
        {
            if(nowPositionY < goalPositionY)
            {
                this.gameObject.transform.Translate(0, 100f, 0);
            }            
        }
        if (raberObject.raberList[lockIndex] == false)
        {
            if (nowPositionY > centerPositionY)
            {
                this.gameObject.transform.Translate(0, -100f, 0);
            }
        }
    }
}
