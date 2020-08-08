using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public PlayerController player;
    public Transform playerPos;
    public NextObject nextObject;    
    public JumpPadObject jumpPadObject;
    public DamageDirector damageDirector;
    public FallObject fallObject;
    public CheckPointObject checkPointObject;
    public WaitObject waitObject;    
    public RollingRockSpan rollingRockSpan;
    public DarkSmogObject darkSmogObject;
    public bool checkSave;      // 세이브포인트 진입을 확인하는 변수    
    public bool fallPadTimerOn = false;     // 타이머 발생 여부 계산용 변수
    public bool waitIn = false;     // 표지판 안의 들어갔음을 확인하는 변수
    
    void Start()
    {        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerPos = GameObject.Find("Player").transform;
        damageDirector = GameObject.Find("DamageDirector").GetComponent<DamageDirector>();
        nextObject = GameObject.Find("NextStage").GetComponent<NextObject>();
        jumpPadObject = GameObject.Find("JumpPad").GetComponent<JumpPadObject>();
        fallObject = GameObject.Find("FallObject").GetComponent<FallObject>();
        waitObject = GameObject.Find("Sign").GetComponent<WaitObject>();
    }
    private void Update()
    {   
        
        if (player.hitObject == "RightBoost" && player.moveDir > 0)
        {
            player.maxSpeed = 6.0f;
        }
        if (player.hitObject == "RightBoost" && player.moveDir < 0)
        {
            player.maxSpeed = 2.0f;
        }
        if (player.hitObject == "RightBoostOut")
        {
            player.maxSpeed = 4.0f;
        }
        if (player.hitObject == "LeftBoost" && player.moveDir < 0)
        {
            player.maxSpeed = 6.0f;
        }
        if (player.hitObject == "LeftBoost" && player.moveDir > 0)
        {
            player.maxSpeed = 2.0f;
        }
        if (player.hitObject == "LeftBoostOut")
        {
            player.maxSpeed = 4.0f;
        }
    }
    public void activeObject()
    {
        Debug.Log(player.hitObject);
        if(player.hitObject == "Next")
        {
            nextObject.NextStage();
        }
        if (player.hitObject == "Quarter")  // 스테이지별 분기 아이템 태그? 수정 필요
        {
            DataController.Instance.gameData.stageOneItemValue += 1;
            Destroy(player.takeObject);
        }
        if (player.hitObject == "JumpPad")
        {
            jumpPadObject.Jump();            
        }
        if (player.hitObject == "Peak" || player.hitObject == "Death")
        {
            damageDirector.OnDameged(player.enemyPosition);
        }
        if (player.hitObject == "Fall")
        {
            fallPadTimerOn = true;
        }
        if (player.hitObject == "FallOut")
        {
            Debug.Log("아웃 진입");
            fallObject.timer = 0;
            fallPadTimerOn = false;
        }
        if (player.hitObject == "Check")
        {
            checkPointObject = GameObject.Find("CheckPoint"+CheckPointObject.checkNum.ToString()).GetComponent<CheckPointObject>();
            checkPointObject.AniIn();
        }
        if (player.hitObject == "Wait")
        {
            if(player.name == "FirstSign"){
                Debug.Log("진입");
                waitIn = true;
            }
        }
        if (player.hitObject == "WaitOut")
        {
            waitIn = false;
            waitObject.waitText.gameObject.SetActive(false);
        }
        if (player.hitObject == "RockIn")
        {
            rollingRockSpan.RockSpan();
        }
        if (player.hitObject == "RockOut")
        {
            rollingRockSpan.RockDestroy();
        }
        if (player.hitObject == "Cave")
        {
            Debug.Log("인 진입");
            darkSmogObject.InsideCace();
        }
        if (player.hitObject == "CaveOut")
        {
            Debug.Log("인 진입");
            darkSmogObject.OutsideCace();
        }
    }
}
