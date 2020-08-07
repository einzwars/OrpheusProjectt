using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{

    PlayerController player;

    void Start() {
        player = new PlayerController();
    }
    public void StageOneStart(){
        // 게임 플레이 씬으로 이동
        // 각각에 맞는 스테이지 활성화
        // 플레이어 위치는 전부 동일
        player.playerPos = new Vector3(-8.55f, -2.92f, 0);
        Debug.Log(player.playerPos);
        SceneManager.LoadScene("Stage1 Complete");
    }

    public void StageTwoStart(){
        player.playerPos = new Vector3(-8.69f, -4.4f, 0);
        SceneManager.LoadScene("Stage2 Complete");
    }

    public void StageThreeStart(){
        SceneManager.LoadScene("Stage3 Complete");        
    }

    public void StageFourStart(){
        SceneManager.LoadScene("Stage4 Complete");        
    }

    public void StageFiveStart(){
        SceneManager.LoadScene("Stage5 Complete");        
    }
}
