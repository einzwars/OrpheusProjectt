using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortReachMon : Monster
{

    // Start is called before the first frame update
    void Start()
    {
        monsterSpeed = 2f;
        moveVelocity = Vector3.right;
    }

    public void AutoMove() // 클래스에서 만든 함수인데 함수만 데리고 오면 작동을 안함
    {
        animator.SetTrigger("Walk");
        if (Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) && moveVelocity == Vector3.right)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            rayDir = Vector3.left;
        }
        else if (Physics2D.OverlapCircle(wallCheck[0].position, 0.1f, layerMask) && moveVelocity == Vector3.left)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            rayDir = Vector3.right;
        }
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;
    }


    public void MonsterAttColliderOnOff()
    {
        monsterAttCollider.SetActive(!monsterAttCollider.activeInHierarchy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AutoMove();

        Vector2 frontVec = new Vector2(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y);
        Debug.DrawRay(frontVec, rayDir, Color.yellow);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, rayDir, 1, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            animator.SetTrigger("Attack");
        }
    }

}
