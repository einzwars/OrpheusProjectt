using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    public StageManager stageManager;
    Animator checkAni;

    void Start()
    {
        checkAni = GetComponent<Animator>();
    }
    void Update()
    {
        if(stageManager.checkSave)
        {
            checkAni.SetTrigger("CheckTrigger");
        }
    }
}
