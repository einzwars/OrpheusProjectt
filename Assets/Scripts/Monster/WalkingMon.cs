using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMon : Monster
{
    

    // Start is called before the first frame update
    void Start()
    {
        monsterSpeed = 2f;

        StartCoroutine("KeepingMove");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

}
