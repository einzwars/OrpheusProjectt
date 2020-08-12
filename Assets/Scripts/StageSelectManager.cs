using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{

    PlayerController player;

    void Start() {
        player = new PlayerController();
    }

    public int maxItem = 5;

    public Image[] stageOneItems;
    public Image[] stageTwoItems;
    public Image[] stageThreeItems;
    public Image[] stageFourItems;
    public Image[] stageFiveItems;

    public Sprite fullQuarter;
    public Sprite emptyQuarter;

    void Update() {
        ItemCollectSystem();    
    }

    void ItemCollectSystem(){      // 아이템 수집 시스템
        if(DataController.Instance.gameData.stageOneItemValue > maxItem){
            DataController.Instance.gameData.stageOneItemValue = maxItem;
        }
        if(DataController.Instance.gameData.stageTwoItemValue > maxItem){
            DataController.Instance.gameData.stageTwoItemValue = maxItem;
        }
        if(DataController.Instance.gameData.stageThreeItemValue > maxItem){
            DataController.Instance.gameData.stageThreeItemValue = maxItem;
        }
        if(DataController.Instance.gameData.stageFourItemValue > maxItem){
            DataController.Instance.gameData.stageFourItemValue = maxItem;
        }
        if(DataController.Instance.gameData.stageFiveItemValue > maxItem){
            DataController.Instance.gameData.stageFiveItemValue = maxItem;
        }

        for(int i=0; i<stageOneItems.Length; i++){
            
            if(i < DataController.Instance.gameData.stageOneItemValue){
                stageOneItems[i].sprite = fullQuarter;
            }
            else{
                stageOneItems[i].sprite = emptyQuarter;
            }

            if(i < maxItem){
                stageOneItems[i].enabled = true;
            }
            else{
                stageOneItems[i].enabled = false;
            }
        }

        for(int i=0; i<stageFourItems.Length; i++){
            
            if(i < DataController.Instance.gameData.stageFourItemValue){
                stageFourItems[i].sprite = fullQuarter;
            }
            else{
                stageFourItems[i].sprite = emptyQuarter;
            }

            if(i < maxItem){
                stageFourItems[i].enabled = true;
            }
            else{
                stageFourItems[i].enabled = false;
            }
        }

        for(int i=0; i<stageTwoItems.Length; i++){
            
            if(i < DataController.Instance.gameData.stageTwoItemValue){
                stageTwoItems[i].sprite = fullQuarter;
            }
            else{
                stageTwoItems[i].sprite = emptyQuarter;
            }

            if(i < maxItem){
                stageTwoItems[i].enabled = true;
            }
            else{
                stageTwoItems[i].enabled = false;
            }
        }

        for(int i=0; i<stageThreeItems.Length; i++){
            
            if(i < DataController.Instance.gameData.stageThreeItemValue){
                stageThreeItems[i].sprite = fullQuarter;
            }
            else{
                stageThreeItems[i].sprite = emptyQuarter;
            }

            if(i < maxItem){
                stageThreeItems[i].enabled = true;
            }
            else{
                stageThreeItems[i].enabled = false;
            }
        }

        for(int i=0; i<stageFiveItems.Length; i++){
            
            if(i < DataController.Instance.gameData.stageFiveItemValue){
                stageFiveItems[i].sprite = fullQuarter;
            }
            else{
                stageFiveItems[i].sprite = emptyQuarter;
            }

            if(i < maxItem){
                stageFiveItems[i].enabled = true;
            }
            else{
                stageFiveItems[i].enabled = false;
            }
        }
    }

    public void StageOneStart(){
        // 게임 플레이 씬으로 이동
        // 각각에 맞는 스테이지 활성화
        SceneManager.LoadScene("Stage1");
    }

    public void StageTwoStart(){
        SceneManager.LoadScene("Stage2");
    }

    public void StageThreeStart(){
        SceneManager.LoadScene("Stage3");        
    }

    public void StageFourStart(){
        SceneManager.LoadScene("Stage4");        
    }

    public void StageFiveStart(){
        SceneManager.LoadScene("Stage5E");        
    }
}
