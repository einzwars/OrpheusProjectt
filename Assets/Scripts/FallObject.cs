using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    StageManager stageManager;
    GameObject target;
    public float timer = 0.0f;       // 낙하 시간 계산용 변수    
    Vector3 objPos;

    // Update is called once per frame
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        objPos = this.gameObject.transform.position;
    }

    void Update()
    {
        target = GameObject.Find(stageManager.player.collisionName);
        if(stageManager.fallPadTimerOn)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.5)                                          // 타이머 변수가 0.6 이상 도달했을 때
        {
            if(target != null && target.name != "Tilemap"){
                target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;   // 리지드바디의 바디 타입을 다이나믹으로 변경            
                Destroy(target, 0.5f);                                                      // 1초 뒤 해당 게임 오브젝트를 파괴
                timer = 0;                                                                             // 타이머 변수를 초기화
                stageManager.fallPadTimerOn = false;                                                                       // 타이머 온 변수를 false로 초기화
            }
        }        
    }

}
