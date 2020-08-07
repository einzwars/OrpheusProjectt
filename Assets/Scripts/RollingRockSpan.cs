using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockSpan : MonoBehaviour
{
    StageManager stageManager;
    GameObject rollingStoneObject;
    GameObject rollingStone;

    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        rollingStoneObject = GameObject.Find("RollingStoneObject");
        rollingStone = rollingStoneObject.transform.Find("RollingStone").gameObject;
    }


    public void RockSpan()
    {
        rollingStone.gameObject.SetActive(true);
    }
    public void RockDestroy()
    {
        rollingStone.gameObject.SetActive(false);
    }
}
