using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAttMon : Monster
{
    // Start is called before the first frame update
    void Start()
    {
            monsterAttCoolTime = 0.5f; // 쿨타임
            monsterAttTimer = 0.5f; // 쿨타임 계산

    }

// Update is called once per frame
void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rb2D.position.x, rb2D.position.y);
        Debug.DrawRay(frontVec, Vector3.left, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.left, 100, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            Fire();
        }
    }
}
