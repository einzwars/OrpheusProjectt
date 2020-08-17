using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeManager : MonoBehaviour
{
    public GameObject gameUI;
    void Start()
    {
        gameUI = GameObject.Find("GamePlayUI");
        // gameUI.transform.Find("ExitPanel").gameObject.SetActive(false);
    }

    public void ShowChoice(){
        Time.timeScale = 0f;
        gameUI.transform.Find("ChoiceMenu").gameObject.SetActive(true);
    }

    public void ResumeBtn(){
        Time.timeScale = 1f;
        gameUI.transform.Find("ChoiceMenu").gameObject.SetActive(false);
    }

    void Update() {
        // if(Application.platform == RuntimePlatform.Android){
            if(Input.GetKeyDown(KeyCode.Escape)){
                ExitPanelShow();
            }
        // }
    }

    public void GameQuit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ExitNo(){
        if(gameUI.transform.Find("ChoiceMenu").gameObject.activeSelf != true)
            Time.timeScale = 1f;
        gameUI.transform.Find("ExitPanel").gameObject.SetActive(false);
    }

    public void ExitPanelShow(){         
        Time.timeScale = 0f;
        gameUI.transform.Find("ExitPanel").gameObject.SetActive(true);
    }

    public void BackBtn(){
        if(SceneManager.GetActiveScene().name == "StageSelectScene"){
            SceneManager.LoadScene("StartScene");
        }
        else{
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
