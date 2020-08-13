using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesMonsterDeath : MonoBehaviour
{    

    public BossObject bossObject;
    public int hadesMonsterNum;
    public int hadesMonsterType;
    public int itemCount = 0;
    public int attCount = 0;

    void Start()
    {
        bossObject = GameObject.Find("HadesObject").GetComponent<BossObject>();
    }
    // Update is called once per frame
    void Update()
    {

    }

}
