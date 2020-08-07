using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSample : MonoBehaviour
{
    float monsterSpeed = 1.0f;
    int life = 3;

    Animator animator;
    Vector3 movement;
    GameObject traceTarget;
    public GameObject notePrefab;
    float delayTime = 1.0f;
    bool isDealy = false;

    bool isTracing;
    int movementFlag = 0; // 0 = 대기, 1 = 왼쪽, 2 = 오른쪽

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0) animator.SetBool("isMoving", false);
        else animator.SetBool("isMoving", true);
        yield return new WaitForSeconds(3f); // 함수가 돌고 5초가 지나면 탈출

        StartCoroutine("ChangeMovement");
    }

    void Attack(){   // 공격 메소드
        if(!isDealy){
            isDealy = true;
            if(transform.localScale.x>0)
                Instantiate(notePrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y, transform.position.z), transform.rotation);
            else if(transform.localScale.x<0)
                Instantiate(notePrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y, transform.position.z), transform.rotation);
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay(){  // 공격 딜레이 코루틴
        yield return new WaitForSeconds(delayTime);
        isDealy = false;
    }

    void Death(){
        if(life == 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            traceTarget = other.gameObject;
            StopCoroutine ("ChangeMovement");
            // Debug.Log("추적 on");
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = true;
            animator.SetBool("isMoving", true);
            // Debug.Log("추적 ing");
            Attack();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            StartCoroutine("ChangeMovement");
            // Debug.Log("추적 end");
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Attack"){
            life -= 1;
            Debug.Log("피격!");
        }
    }

    private void FixedUpdate() // 설정된 값에 따라 일정 간격으로 호출하며 물리효과가 적용된 오브젝스틑 적용할 때 사용
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";
        if(isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;
            if (playerPos.x < transform.position.x) dist = "Left";
            else if (playerPos.x > transform.position.x) dist = "Right";
        }
        else
        {
            if (movementFlag == 1) dist = "Left";
            else if (movementFlag == 2) dist = "Right";

        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left; // Vector3.left = (-1,0,0)
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position += moveVelocity * monsterSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }
}
