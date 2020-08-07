using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    public int BGMSound = 0;
    public int EffectSound = 0;

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
}
