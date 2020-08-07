using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObject : MonoBehaviour
{
    public int stageIndex;    
    public GameDirector gameDirector;            // 디렉터 대입
    public GameObject[] Stages;

    public void NextStage()  // 1번 분기일 때 스테이지를 이동하는 변수
    {
        Stages[stageIndex].SetActive(false);      // 현재 스테이지를 가림
        Stages[stageIndex + 4].SetActive(false);  // 분기 시 현재 스테이지를 가림
        stageIndex++;                             // 스테이지 인덱스 변수 추가
        Stages[stageIndex].SetActive(true);       // 다음 스테이지를 보이도록 한다
        gameDirector.PlayerReposition();                       // 플레이어를 스테이지 출발지로 이동
    }
    public void OtherStage()  // 2번 분기일 때 스테이지를 이동하는 변수
    {
        Stages[stageIndex].SetActive(false);
        Stages[stageIndex + 4].SetActive(false);
        stageIndex++;
        Stages[stageIndex + 4].SetActive(true);  // 2번 분기 목록의 스테이지를 보이도록 한다
        gameDirector.PlayerReposition();
    }











}
