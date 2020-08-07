using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    StageManager stageManager;
    public float timer = 0.0f;       // 낙하 시간 계산용 변수    

    // Update is called once per frame
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        if(stageManager.fallPadTimerOn)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)                                          // 타이머 변수가 0.6 이상 도달했을 때
        {            
            stageManager.player.takeObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;   // 리지드바디의 바디 타입을 다이나믹으로 변경            
            Destroy(stageManager.player.takeObject, 0.5f);                                                      // 1초 뒤 해당 게임 오브젝트를 파괴
            timer = 0;                                                                             // 타이머 변수를 초기화
            stageManager.fallPadTimerOn = false;                                                                       // 타이머 온 변수를 false로 초기화
        }        
    }

}
