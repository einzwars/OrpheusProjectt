using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    StageManager stageManager;
    Animator checkAni;
    public int checkNum;

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        checkAni = gameObject.GetComponent<Animator>();
    }
   
    public void AniIn()
    {
        checkAni.SetTrigger("CheckTrigger");
    }

    public void SaveSuccess(){
        checkAni.SetTrigger("SaveTrigger");
    }
}
