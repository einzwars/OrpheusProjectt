using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPeakObject : MonoBehaviour
{    
    // 해당 스크립트는 적용할 오브젝트에 넣으면 된다

    float timer = 0.0f;       // 돌출 시간 계산용 변수
    public bool change;
    public bool first;
    float centerPositionX;
    float centerPositionY;
    float goalPositionX;
    float goalPositionY;
    float moveLeach = 0.23f;
    float nowPositionX;
    float nowPositionY;


    // Start is called before the first frame update
    void Start()
    {
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        goalPositionX = centerPositionX + moveLeach;
        goalPositionY = centerPositionY + moveLeach;
    }

    // Update is called once per frame
    void Update()
    {
        nowPositionX = gameObject.transform.position.x;
        nowPositionY = gameObject.transform.position.y;
        timer += Time.deltaTime;
        if(first)
        {
            if (!change)
            {
                if (timer > 5)//올리기
                {
                    if (nowPositionY > goalPositionY)
                    {
                        change = true;
                        timer = 0.0f;
                    }
                    gameObject.transform.Translate(0, 0.005f, 0);
                }
            }
            if (change)
            {
                if (timer > 3)//내리기
                {
                    if (nowPositionY < centerPositionY)
                    {
                        if (timer > 5)
                        {
                            change = false;
                            timer = 0.0f;
                        }
                        gameObject.transform.Translate(0, 0.005f, 0);
                    }
                    gameObject.transform.Translate(0, -0.005f, 0);
                }
            }

        }
        if (!first)
        {
            if (change)
            {
                if (timer > 5)//올리기
                {
                    if (nowPositionY > goalPositionY)
                    {
                        change = false;
                        timer = 0.0f;
                    }
                    gameObject.transform.Translate(0, 0.005f, 0);
                }
            }
            if (!change)
            {
                if (timer > 3)//내리기
                {
                    if (nowPositionY < centerPositionY)
                    {
                        if (timer > 5)
                        {
                            change = true;
                            timer = 0.0f;
                        }
                        gameObject.transform.Translate(0, 0.005f, 0);
                    }
                    gameObject.transform.Translate(0, -0.005f, 0);
                }
            }
        }
    }
}
