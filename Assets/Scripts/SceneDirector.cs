﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneDirector : MonoBehaviour
{
    AudioSource audio;
    public Sprite newSprite;
    public Sprite originalSprite;
    private Button button;
    GameObject canvas;
    GameObject exitPanel;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        canvas = GameObject.Find("Canvas");
        canvas.transform.Find("ExitPanel").gameObject.SetActive(false);
        // exitPanel.SetActive(false);
    }

    void Update() {
        // if(Application.platform == RuntimePlatform.Android){
            if(Input.GetKeyDown(KeyCode.Escape)){
                ExitPanelShow();
            }
        // }
    }

    public void ExitPanelShow(){         
        Time.timeScale = 0f;
        canvas.transform.Find("ExitPanel").gameObject.SetActive(true);  // 나가기 패널 호출
    }

    public void StageSelect(){
        SceneManager.LoadScene("StageSelectScene");
    }

    public void GameQuit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ExitNo(){
        Time.timeScale = 1f;
        canvas.transform.Find("ExitPanel").gameObject.SetActive(false);
    }

    public void GameMute(){
        if(audio.mute){
            audio.mute = false;
            button.image.sprite = originalSprite;
        }
        else{
            audio.mute = true;
            button.image.sprite = newSprite;
        }
    }
}
