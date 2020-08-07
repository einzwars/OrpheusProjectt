using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    StageManager stageManager;
    Animator checkAni;

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        checkAni = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
    }
    public void AniIn()
    {
        checkAni.SetTrigger("CheckTrigger");
    }
}
