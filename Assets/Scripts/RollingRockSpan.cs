using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockSpan : MonoBehaviour
{
    StageManager stageManager;    
    public int stoneSponIndex;
    public bool[] stoneList;
    public GameObject stoneObject;
    public GameObject stone;

    
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        stoneObject = GameObject.Find("RollingStoneObject");
    }


    public void RockSpan()
    {
        stoneList[stoneSponIndex] = true;
    }
    public void RockDestroy()
    {
        stoneList[stoneSponIndex] = false;
    }
    private void Update()
    {
        stone = stoneObject.transform.Find("RollingStone" + stoneSponIndex).gameObject;
        
        if (stoneList[stoneSponIndex] == true)
        {            
            stone.gameObject.SetActive(true);
        }

        if (stoneList[stoneSponIndex] == false)
        {
            stone.gameObject.SetActive(false);
        }
    }

}
