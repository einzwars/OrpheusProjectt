using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuarterItem : MonoBehaviour
{
    public int qtNum;

    void Start()
    {
        this.gameObject.name = "QuarterItem" + qtNum.ToString();
        if (SceneManager.GetActiveScene().name == "Stage1 Scenario")
        {
            if (qtNum == 1) {
                if (DataController.Instance.gameData.stageOneItem1)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 2)
            {
                if (DataController.Instance.gameData.stageOneItem2)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 3)
            {
                if (DataController.Instance.gameData.stageOneItem3)
                    Destroy(this.gameObject);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage2 Scenario")
        {
            if (qtNum == 1)
            {
                if (DataController.Instance.gameData.stageTwoItem1)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 2)
            {
                if (DataController.Instance.gameData.stageTwoItem2)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 3)
            {
                if (DataController.Instance.gameData.stageTwoItem3)
                    Destroy(this.gameObject);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage3 Scenario")
        {
            if (qtNum == 1)
            {
                if (DataController.Instance.gameData.stageThreeItem1)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 2)
            {
                if (DataController.Instance.gameData.stageThreeItem2)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 3)
            {
                if (DataController.Instance.gameData.stageThreeItem3)
                    Destroy(this.gameObject);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage4 Scenario")
        {
            if (qtNum == 1)
            {
                if (DataController.Instance.gameData.stageFourItem1)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 2)
            {
                if (DataController.Instance.gameData.stageFourItem2)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 3)
            {
                if (DataController.Instance.gameData.stageFourItem3)
                    Destroy(this.gameObject);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage5 Scenario")
        {
            if (qtNum == 1)
            {
                if (DataController.Instance.gameData.stageFiveItem1)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 2)
            {
                if (DataController.Instance.gameData.stageFiveItem2)
                    Destroy(this.gameObject);
            }
            else if (qtNum == 3)
            {
                if (DataController.Instance.gameData.stageFiveItem3)
                    Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Stage1 Scenario")
            {
                DataController.Instance.gameData.stageOneItemValue += 1;
                if (qtNum == 1)
                    DataController.Instance.gameData.stageOneItem1 = true;
                else if(qtNum == 2)
                    DataController.Instance.gameData.stageOneItem2 = true;
                else if(qtNum == 3)
                    DataController.Instance.gameData.stageOneItem3 = true;
            }
            if (SceneManager.GetActiveScene().name == "Stage2 Scenario")
            {
                DataController.Instance.gameData.stageTwoItemValue += 1;
                if (qtNum == 1)
                    DataController.Instance.gameData.stageTwoItem1 = true;
                else if (qtNum == 2)
                    DataController.Instance.gameData.stageTwoItem2 = true;
                else if (qtNum == 3)
                    DataController.Instance.gameData.stageTwoItem3 = true;
            }
            if (SceneManager.GetActiveScene().name == "Stage3 Scenario")
            {
                DataController.Instance.gameData.stageThreeItemValue += 1;
                if (qtNum == 1)
                    DataController.Instance.gameData.stageThreeItem1 = true;
                else if (qtNum == 2)
                    DataController.Instance.gameData.stageThreeItem2 = true;
                else if (qtNum == 3)
                    DataController.Instance.gameData.stageThreeItem3 = true;
            }
            if (SceneManager.GetActiveScene().name == "Stage4 Scenario")
            {
                DataController.Instance.gameData.stageFourItemValue += 1;
                if (qtNum == 1)
                    DataController.Instance.gameData.stageFourItem1 = true;
                else if (qtNum == 2)
                    DataController.Instance.gameData.stageFourItem2 = true;
                else if (qtNum == 3)
                    DataController.Instance.gameData.stageFourItem3 = true;
            }
            if (SceneManager.GetActiveScene().name == "Stage5 Scenario")
            {
                DataController.Instance.gameData.stageFiveItemValue += 1;
                if (qtNum == 1)
                    DataController.Instance.gameData.stageFiveItem1 = true;
                else if (qtNum == 2)
                    DataController.Instance.gameData.stageFiveItem2 = true;
                else if (qtNum == 3)
                    DataController.Instance.gameData.stageFiveItem3 = true;
            }
            Destroy(this.gameObject);
        }
    }
}
