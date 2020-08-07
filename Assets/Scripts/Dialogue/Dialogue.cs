using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("카메라가 타겟팅할 대상")]
    public Transform tf_Target;

    [Tooltip("대사치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;
}

[System.Serializable]
public class DialogueEvent
{
    // 이벤트 명
    public string name;

    // x번째 줄부터 y번째 줄까지 불러옴
    public Vector2 line;
    public Dialogue[] dialogues;
}
