using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]// 클래스 자체를 변수로 받았기 때문에 강제로 Inspector창에 띄우기 위해 필요
public class Sound {
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    int sceneNum;
    [Header("사운드 등록")] // Inspector 창을 보기 쉽게 만들기 위해
    [SerializeField] Sound[] bgmSound; // 사운드를 Inspector창에서 수정할 수 있게 해주기 위함
    // 브금은 여러개 일 수 있기 때문에 배열로 제작

    [Header("브금 플레이어")] // 이름
    [SerializeField] AudioSource bgmPlayer;// 오디오 파일을 재생하는 플레이어
    //AudioSource << 이게 플레이어 역할 여기에 오디오 소스들을 집어 넣어 음악이 흘러나오게


    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Stage1 Scenario")
            sceneNum = 1;
        else if(SceneManager.GetActiveScene().name == "Stage2 Scenario")
            sceneNum = 2;
        else if(SceneManager.GetActiveScene().name == "Stage3 Scenario")
            sceneNum = 3;
        else if(SceneManager.GetActiveScene().name == "Stage4 Scenario")
            sceneNum = 4;
        else if(SceneManager.GetActiveScene().name == "Stage5 Scenario")
            sceneNum = 5;
        else if(SceneManager.GetActiveScene().name == "StartScene")
            sceneNum = 6;

        PlayBGM(sceneNum);
    }

    public void PlayBGM(int sceneNum){
        bgmPlayer.clip = bgmSound[sceneNum-1].clip;
        bgmPlayer.Play(); // 음악 플레이
    }
}