using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextObject : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void NextStage()
    {
        if(this.gameObject.name == "Ending"){
            int totalQtItem = DataController.Instance.gameData.stageOneItemValue + DataController.Instance.gameData.stageTwoItemValue + DataController.Instance.gameData.stageThreeItemValue + DataController.Instance.gameData.stageFourItemValue + DataController.Instance.gameData.stageFiveItemValue;
            DataController.Instance.gameData.stageFiveClear = true;
            if(totalQtItem == 15){
                SceneManager.LoadScene("Ending");
            }
            else
                SceneManager.LoadScene("BadEnding");
        }
        else if(this.gameObject.name == "Stage1 Clear"){
            DataController.Instance.gameData.stageOneClear = true;
            SceneManager.LoadScene("StageSelectScene");
        }
        else if(this.gameObject.name == "Letter"){
            DataController.Instance.gameData.stageTwoClear = true;
            SceneManager.LoadScene("StageSelectScene");
        }
        else if(this.gameObject.name == "Stage3 Clear"){
            DataController.Instance.gameData.stageThreeClear = true;
            SceneManager.LoadScene("StageSelectScene");
        }
        else if(this.gameObject.name == "Stage4 Clear"){
            DataController.Instance.gameData.stageFourClear = true;
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
