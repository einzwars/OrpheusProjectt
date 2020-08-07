using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public PlayerController player;    
    public NextObject nextObject;
    public QuarterObject quarterObject;
    public JumpPadObject jumpPadObject;
    public DamageDirector damageDirector;
    public FallObject fallObject;
    public CheckPointObject checkPointObject;
    public bool jumpCheck;      // 점프대를 진행했는지 확인하는 변수
    public bool checkSave;      // 세이브포인트 진입을 확인하는 변수
    public float jumpPadFirst;  // 최초 점프값
    public bool fallPadTimerOn = false;     // 타이머 발생 여부 계산용 변수


    public void activeObject()
    {
        Debug.Log(player.hitObject);
        if(player.hitObject == "Next")
        {
            if (player.quarterPoint < 1)
            {
                nextObject.NextStage();
            }
            if (player.quarterPoint == 1)
            {
                nextObject.OtherStage();
            }
        }
        if (player.hitObject == "Quarter")
        {
            quarterObject.QuaterPointUp();          
        }
        if (player.hitObject == "JumpPad")
        {
            jumpCheck = true;                           // 점프 확인 변수를 트루로 변경
            jumpPadFirst = player.transform.position.y; // 캐릭터의 현재 y값을 기록한다
        }
        if (player.hitObject == "SpeedUp")
        {
            player.maxSpeed = 10.0f;
        }
        if (player.hitObject == "SpeedUpOut")
        {
            player.maxSpeed = 5.0f;
        }
        if (player.hitObject == "SpeedDown")
        {
            player.maxSpeed = 2.5f;
        }
        if (player.hitObject == "SpeedDownOut")
        {
            player.maxSpeed = 5.0f;
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
            fallObject.timer = 0;
            fallPadTimerOn = false;
        }
        if (player.hitObject == "Check")
        {
            checkSave = true;
        }
    }






}
