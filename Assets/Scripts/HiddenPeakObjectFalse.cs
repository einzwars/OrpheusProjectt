using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPeakObjectFalse : MonoBehaviour
{
    // 해당 스크립트는 적용할 오브젝트에 넣으면 된다

    float timer = -1.0f;       // 돌출 시간 계산용 변수
    public bool change;
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
        goalPositionX = centerPositionX - moveLeach;
        goalPositionY = centerPositionY - moveLeach;
    }

    // Update is called once per frame
    void Update()
    {
        nowPositionX = gameObject.transform.position.x;
        nowPositionY = gameObject.transform.position.y;
        timer += Time.deltaTime;
        if (timer > 3)
        {
            if (!change)
            {
                if (nowPositionY < goalPositionY)
                {
                    change = true;
                    timer = 0.0f;
                }
                gameObject.transform.Translate(0, -0.005f, 0);
            }
            if (change)
            {
                if (nowPositionY > centerPositionY)
                {
                    change = false;
                    timer = 0.0f;
                }
                gameObject.transform.Translate(0, 0.005f, 0);
            }
        }
    }

}
