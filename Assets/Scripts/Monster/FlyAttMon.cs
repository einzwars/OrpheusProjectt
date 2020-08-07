﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttMon : Monster

{
    // Start is called before the first frame update
    void Start()
    {
        monsterHP = 3;
        monsterAttCoolTime = 1.0f; // 쿨타임
        monsterAttTimer = 1.0f; // 쿨타임 계산

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Attack"))
        {
            TakeDamage();
            animator.SetTrigger("Hit");
            hitAniPlay = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rb2D.position.x, rb2D.position.y);
        Debug.DrawRay(frontVec, Vector3.left, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.left, 100, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            animator.SetTrigger("Attack");

        }
        else if (rayHit.collider == null)
        {
            animator.SetTrigger("Idle");

        }

        MonsterDeath();

    }


}