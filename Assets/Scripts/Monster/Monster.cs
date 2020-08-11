using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float playerDamage = 1;


    public float monsterHP;
    public float monsterMaxHP;
    public float monsterAtt;
    public float monsterAttSpeed;
    public float monsterDef;
    public float monsterSpeed;
    public float monsterJumpPower;

    public float monsterAttCoolTime; // 쿨타임
    public float monsterAttTimer; // 쿨타임 계산

    public bool isHit;
    public bool hitAniPlay;
    public bool isMoving;
    public bool canAtt = true;
    public bool monsterDirRight = true; // 뭔지 아직 모름

    public int monsterAttType; // 0 = 근거리, 1 = 원거리
    public int monsterMoveType; // 0 = 땅, 1 = 하늘
    public int monsterMoveFlag = 0; // 0 = Idle, 1 = Left, 2 = Right

    public Rigidbody2D rb2D;
    public GameObject monsterAttCollider; // 몬스터 타격 범위
    public GameObject hitBoxCollider; // Player가 공격했을 때 맞는 부분
    public Animator animator;
    public GameObject Bullet;

    // LayerMask.GetMask("layerName")
    public LayerMask layerMask; // Ray가 감지해야하는 레이어 체커
    public Transform[] wallCheck; // 벽에 닿으면 방향전환 하는 몬스터 용 콜라이더 체커
    public Transform genPoint;

    public Vector3 moveVelocity; // 초기 움직임 방향
    public Vector3 rayDir; // Ray 쏘는 방향

    public SpriteRenderer sr;


    protected void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(ResetCollider());
        StartCoroutine(CalcAttDelay());
        StartCoroutine(CalcHitAniDelay());

    }


    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            // if (!hitBoxCollider.activeInHierarchy)
            // {
            //     yield return new WaitForSeconds(0.5f);
            //     hitBoxCollider.SetActive(true);
            //     isHit = false;
            // }
        }
    }
    IEnumerator CalcAttDelay()
    {
        while (true)
        {
            yield return null;
            if (!canAtt)
            {
                animator.SetTrigger("Idle");


                monsterAttTimer -= Time.deltaTime;
                if (monsterAttTimer <= 0)
                {

                    monsterAttTimer = monsterAttCoolTime;
                    canAtt = true;
                }
            }
        }
    }
    IEnumerator CalcHitAniDelay()
    {
        while (true)
        {
            yield return null;
            if (hitAniPlay)
            {
                yield return new WaitForSeconds(0.03f);
                animator.SetTrigger("Idle");
                hitAniPlay = false;
            }
        }
    }



    IEnumerator KeepingMove()
    {
        monsterMoveFlag = Random.Range(0, 3);
        Debug.Log(monsterMoveFlag);
        yield return new WaitForSeconds(2f); // 함수가 돌고 5초가 지나면 탈출
        StartCoroutine("KeepingMove");

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
        }
        else
        {
            animator.SetTrigger("Walk");
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Attack"))
        {
            Debug.Log("몬스터 맞음!");
            TakeDamage();
            animator.SetTrigger("Hit");
            hitAniPlay = true;
        }
    }



    public void TakeDamage() // 데미지를 받으면 hitboxcollider를 끔 --- hitboxcollider를 인스펙터에서 넣어줘야 함
    {
        monsterHP -= playerDamage;
        isHit = true;
        hitBoxCollider.SetActive(false);
    }

    public void MonsterDeath()
    {
        if(monsterHP <= 0 )
        {
            Destroy(this.gameObject);
            Debug.Log("주금");
        }
    }

    public void Fire()
    {
        if(canAtt)
        {

            if (sr.flipX)    // true면 원래의 반대방향 보고 있는 것
            {
                GameObject MonAtt = Instantiate(Bullet, genPoint.position, transform.rotation);

                MonAtt.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
                MonAtt.transform.localScale = new Vector2(transform.localScale.x, 1f);

                Destroy(MonAtt, 2);

            }
            else if(!sr.flipX)
            {
                GameObject MonAtt = Instantiate(Bullet, genPoint.position, transform.rotation);

                MonAtt.GetComponent<Rigidbody2D>().velocity = transform.right * transform.localScale.x * 10f;
                MonAtt.transform.localScale = new Vector2(transform.localScale.x, 1f);
                Destroy(MonAtt, 2);

            }
            canAtt = false;
        }
    }

    public void Fire2()
    {
        if (canAtt)
        {

            if (!sr.flipX)    // true면 원래의 반대방향 보고 있는 것
            {
                GameObject MonAtt = Instantiate(Bullet, genPoint.position, transform.rotation);

                MonAtt.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
                MonAtt.transform.localScale = new Vector2(transform.localScale.x, 1f);

                Destroy(MonAtt, 2);

            }
            else if (sr.flipX)
            {
                GameObject MonAtt = Instantiate(Bullet, genPoint.position, transform.rotation);

                MonAtt.GetComponent<Rigidbody2D>().velocity = transform.right * transform.localScale.x * 10f;
                MonAtt.transform.localScale = new Vector2(transform.localScale.x, 1f);
                Destroy(MonAtt, 2);

            }
            canAtt = false;
        }
    }


    //public void Tracing()
    //{
    //    Vector3 moveVelocity = Vector3.zero;
    //    string dist = "";
    //    if (isTracing)
    //    {
    //        Vector3 playerPos = traceTarget.transform.position;
    //        if (playerPos.x < transform.position.x) dist = "Left";
    //        else if (playerPos.x > transform.position.x) dist = "Right";
    //    }
    //    else
    //    {
    //        if (movementFlag == 1) dist = "Left";
    //        else if (movementFlag == 2) dist = "Right";

    //    }

    //    if (dist == "Left")
    //    {
    //        moveVelocity = Vector3.left; // Vector3.left = (-1,0,0)
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    }
    //    else if (dist == "Right")
    //    {
    //        moveVelocity = Vector3.right;
    //        transform.localScale = new Vector3(1, 1, 1);
    //    }
    //    transform.position += moveVelocity * monsterSpeed * Time.deltaTime;
    //}

}
