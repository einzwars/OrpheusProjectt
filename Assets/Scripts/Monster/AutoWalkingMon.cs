using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWalkingMon : Monster
{

    // Start is called before the first frame update
    void Start()
    {
        monsterHP = 3;

        monsterSpeed = 2f;
        moveVelocity = Vector3.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) && moveVelocity == Vector3.right)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) && moveVelocity == Vector3.left)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);

        }
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;


        MonsterDeath();


    }
}
