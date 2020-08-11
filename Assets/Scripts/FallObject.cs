using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    StageManager stageManager;
    GameObject player;
    GameObject target;
    public float timer = 0.0f;       // 낙하 시간 계산용 변수
    public float activeTimer = 0.0f; // 활성화 시간 계산용 변수

    
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {        
        target = GameObject.Find(stageManager.player.collisionName);

        if(target != null && player.transform.position.y > target.transform.position.y && stageManager.fallPadTimerOn)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.8)                                          // 타이머 변수가 0.6 이상 도달했을 때
        {
            if(target != null && target.tag == "Fall" && player.transform.position.y > target.transform.position.y){
                activeTimer += Time.deltaTime;
                target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;   // 리지드바디의 바디 타입을 다이나믹으로 변경            
                if(activeTimer >= 0.004f){
                    // Destroy(target, 0.5f);                                                      // 1초 뒤 해당 게임 오브젝트를 비활성화
                    // target.gameObject.SetActive(false);
                    target.GetComponent<BoxCollider2D>().enabled = false;
                    target.GetComponent<Renderer>().enabled = false;
                    target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;          
                }
                activeTimer = 0;
                timer = 0;                                                                             // 타이머 변수를 초기화
                stageManager.fallPadTimerOn = false;                                                   // 타이머 온 변수를 false로 초기화
            }
        }
    }
}
