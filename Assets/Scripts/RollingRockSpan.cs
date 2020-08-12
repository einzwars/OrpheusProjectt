using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockSpan : MonoBehaviour
{
    StageManager stageManager;
    public int stoneSponIndex;
    public GameObject[] stoneList;     
    
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    public void RockSpan()
    {       
        stoneList[stoneSponIndex].SetActive(true);
    }
    public void RockDestroy()
    {
        stoneList[stoneSponIndex].SetActive(false);
    }
    private void Update()
    {
        
    }

}
