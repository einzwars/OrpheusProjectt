using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesMonsterDeath : MonoBehaviour
{
    public Monster monster;
    public BossObject bossObject;
    public int hadesMonsterNum;
    public int itemCount = 0;
    public int attCount = 0;

    void Start()
    {
        monster = GetComponent<Monster>();
        bossObject = GameObject.Find("HadesObject").GetComponent<BossObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if(monster.monsterHP <= 0)
        {
            bossObject.monsterIndex = hadesMonsterNum;
            bossObject.monsterDeath[bossObject.monsterIndex] = true;
        }
    }
}
