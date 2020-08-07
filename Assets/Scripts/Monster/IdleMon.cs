using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMon : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        monsterHP = 3;
    }


    // Update is called once per frame
    void Update()
    {
        MonsterDeath();
    }
}
