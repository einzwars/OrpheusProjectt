using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextObject : MonoBehaviour
{
    public int loadSceneNum;
    PlayerController player;
    Transform startPosition;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        startPosition = GameObject.Find("StartPositionObject").GetComponent<Transform>();
    }

    public void NextStage()  // 1번 분기일 때 스테이지를 이동하는 변수
    {
        player.transform.position = startPosition.transform.position;
        SceneManager.LoadScene("Stage" + loadSceneNum);
    }











}
