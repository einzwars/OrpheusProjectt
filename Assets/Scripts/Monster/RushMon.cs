using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushMon : Monster
{


    // Start is called before the first frame update
    void Start()
    {
        monsterSpeed = 5f;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
        transform.localScale = new Vector3(-1, 1, 1);
        moveVelocity = Vector3.left;

        monsterAttCoolTime = 3; // 쿨타임
        monsterAttTimer = 3; // 쿨타임 계산

    }
    public void MonsterAttColliderOnOff()
    {
        monsterAttCollider.SetActive(!monsterAttCollider.activeInHierarchy);
    }


    void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y);
        Debug.DrawRay(frontVec, Vector3.left, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.left, 6, LayerMask.GetMask("Player"));
        if (rayHit.collider != null && canAtt)
        {
            animator.SetTrigger("Attack");
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += moveVelocity * monsterSpeed * Time.deltaTime;
        }
        else if (rayHit.collider == null)
        {
            animator.SetTrigger("Idle");
        }

    }
}
