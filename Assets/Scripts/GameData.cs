using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    public int BGMSound = 0;
    public int EffectSound = 0;

    public bool prologueView = false;   // 프롤로그 봤음?
    public bool stageOneClear = false;  // 스테이지1 클리어 여부
    public bool stageTwoClear = false;  // 스테이지2 클리어 여부
    public bool stageThreeClear = false;  // 스테이지3 클리어 여부
    public bool stageFourClear = false;  // 스테이지4 클리어 여부
    public bool stageFiveClear = false;  // 스테이지5 클리어 여부
    
    public int stageOneItemValue = 0; // 1스테이지 분기 아이템 수집 개수
    public int stageTwoItemValue = 0; // 2스테이지 분기 아이템 수집 개수
    public int stageThreeItemValue = 0; // 3스테이지 분기 아이템 수집 개수
    public int stageFourItemValue = 0; // 4스테이지 분기 아이템 수집 개수
    public int stageFiveItemValue = 0; // 5스테이지 분기 아이템 수집 개수

    public bool stageOneItem1 = false;
    public bool stageOneItem2 = false;
    public bool stageOneItem3 = false;
    public bool stageTwoItem1 = false;
    public bool stageTwoItem2 = false;
    public bool stageTwoItem3 = false;
    public bool stageThreeItem1 = false;
    public bool stageThreeItem2 = false;
    public bool stageThreeItem3 = false;
    public bool stageFourItem1 = false;
    public bool stageFourItem2 = false;
    public bool stageFourItem3 = false;
    public bool stageFiveItem1 = false;
    public bool stageFiveItem2 = false;
    public bool stageFiveItem3 = false;
}
