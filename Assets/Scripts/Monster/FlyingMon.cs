using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMon : Monster
{
    bool up;

    // Start is called before the first frame update
    void Start()
    {
        monsterSpeed = 1.0f;
        monsterHP = 3;

        StartCoroutine("Flying");
    }
    IEnumerator Flying()
    {
        if(up)
        {
            moveVelocity = Vector3.up;
            yield return new WaitForSeconds(1.0f); // 함수가 돌고 5초가 지나면 탈출
            up = false;
        }
        else if (!up)
        {
            moveVelocity = Vector3.down;
            yield return new WaitForSeconds(1.0f); // 함수가 돌고 5초가 지나면 탈출
            up = true;

        }
        // Debug.Log(up);
        StartCoroutine("Flying");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;

        MonsterDeath();

    }
}
