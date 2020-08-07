using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMon : Monster
{

   

    IEnumerator KeepingMove()
    {
        monsterSpeed = 2f;
        monsterMoveFlag = Random.Range(0, 3);
        yield return new WaitForSeconds(2f); // 함수가 돌고 5초가 지나면 탈출

        StartCoroutine("KeepingMove");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        Vector2 frontVec = new Vector2(rb2D.position.x, rb2D.position.y);
        Debug.DrawRay(frontVec, rayDir, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, rayDir, 25, LayerMask.GetMask("Player"));
        if(rayHit.collider != null)
        {
            Fire();
        }
        
    }

    public void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (monsterMoveFlag == 0)
        {
            animator.SetTrigger("Idle");
        }
        else if (monsterMoveFlag == 1)
        {
            animator.SetTrigger("Walk");
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            rayDir = Vector3.left;
        }
        else
        {
            animator.SetTrigger("Walk");
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            rayDir = Vector3.right;
        }
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;

    }

}

