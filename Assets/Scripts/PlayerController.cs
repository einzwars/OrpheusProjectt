﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 관련 필드
    public float moveSpeed;
    public float maxSpeed;
    public bool isGround;
    public float moveDir;
    public float jumpPower;
    public float AttackSlideRate;
    public float slideRate;
    private float refVelocity = 0.0f;
    float delayTime = 1.0f;
    bool doubleJump = true;
    public Transform dir;
    
    // 대시
    public float dashForce = 500.0f;
    bool dashChance = true;
    bool dashCountDown = false;
    float dashTimer = 0.0f;
    float dashCooltime = 2.0f;  

    // 플레이어 조작 트리거
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputJump = false;    
    bool isUnBeatTime;

    // 플레이어 스탯
    public int life;
    public int maxLife;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    bool isDelay = false;
    Vector3 savePoint;  // 플레이어 세이브포인트 위치 저장

    // 맵 상호작용 필드
    public GameObject takeObject;          // 플레이어와 맞닿은 대상 대입
    public GameDirector gameDirector;      // 디렉터 대입
    public StageManager stageManager;      // 스테이지 매니저 대입
    public DamageDirector damageDirector;      // 데미지 디렉터 대입
    public Transform playerPos;            // 플레이어 포지션 대입
    public Collider2D playerCol; // Collider2D 컴포넌트를 참조하기 위한 변수
    public string hitObject;  // 플레이어가 맞닿은 오브젝트
    public int quarterPoint;       // 분기 포인트
    public Vector2 enemyPosition;


    // 유니티 시스템 참조
    public Rigidbody2D rb;
    BoxCollider2D[] playerCollider;
    public SpriteRenderer sr;
    public Animator anim;
    public ItemManager itemManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponents<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        savePoint = new Vector3(1, -8.04f, 1);
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        MoveManager moveBtn = GameObject.Find("MoveManager").GetComponent<MoveManager>();
        moveBtn.Init();
    }

    void Update()
    {
        PlayerInput();  // 이동
        GroundCheck();
        PlayerAnim();
        GroundFriction();

        // 모바일 이동
        MoveLeft();
        MoveRight();

        LifeSystem();
        Death();

        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
        if(Input.GetKeyDown(KeyCode.Z))
            Dash();

        if(dashCountDown && dashTimer >= dashCooltime){
            dashTimer = 0;
            dashCountDown = false;
        }
        else if(dashCountDown){
            dashTimer += Time.deltaTime;
        }       

        if(transform.position.y < -10){
            life = 0;
            gameDirector.PlayerReposition();
        }
    }

    private void FixedUpdate() {
        if(!IsPlayingAnim("Attack")){
            if(PlayerFlip() || Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed){
                rb.AddForce(new Vector2(moveDir * Time.fixedDeltaTime * moveSpeed, 0));
            }
            else
            {
                rb.velocity = new Vector2(moveDir * maxSpeed, rb.velocity.y);
            }
        }
    }

    public void MoveLeft(){
        if(inputLeft){
                if(Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed || rb.velocity.x > 0){
                    Debug.Log(rb.velocity.x);
                    moveDir = -1;
            }
        }
    }

    public void MoveRight(){
        if(inputRight){
                if(Mathf.Abs(moveDir * rb.velocity.x) < maxSpeed || rb.velocity.x < 0){
                    Debug.Log(rb.velocity.x);
                    moveDir = 1;
                }
            }
        }

    void LifeSystem(){      // 플레이어 라이프 시스템
        if(life>maxLife){
            life = maxLife;
        }

        for(int i=0; i<hearts.Length; i++){
            
            if(i < life){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxLife){
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
    }

    void PlayerInput(){
        moveDir = Input.GetAxisRaw("Horizontal");
        if(inputJump && isGround && !IsPlayingAnim("Attack")){    
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.X)){
            Attack();
        }
    }

    public void Jump(){    // 점프 메소드
        if(rb.velocity.y == 0){
            // rb.AddForce(transform.up*jumpPower, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            MyAnimSetTrigger("Jump");
            doubleJump = true; 
        }
        else if(doubleJump && rb.velocity.y!=0){
            Debug.Log("이단점프!");
            // rb.AddForce(transform.up*jumpPower, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            MyAnimSetTrigger("Jump");
            doubleJump = false;
        }
    }

    public void Dash(){    // 대쉬 메소드        
        if(rb.velocity.x>0 && dashChance && !dashCountDown){
            rb.AddForce(transform.right*dashForce);
            dashChance = false;
            dashCountDown = true;
        }
        if(rb.velocity.x<0 && dashChance && !dashCountDown){
            rb.AddForce(transform.right*-dashForce);
            dashChance = false;
            dashCountDown = true;
        }
    }

    public void Attack(){   // 테스트용 공격 메소드
        if(itemManager.pomul == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX)
                Instantiate(itemManager.pomulPrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, 30));
            else if(sr.flipX)
                Instantiate(itemManager.pomulPrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, -30));
            itemManager.ConsumeBullet();
            StartCoroutine(AttackDelay());
        }
        else if(itemManager.threeBall == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX){
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, -10));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, 0));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, 10));
            }
            else if(sr.flipX){
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, -10));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, 0));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z), Quaternion.Euler(0, 0, 10));
            }
            itemManager.ConsumeBullet();
            StartCoroutine(AttackDelay());
        }
        else if(itemManager.basicAtk == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX)
                Instantiate(itemManager.notePrefab as GameObject, new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z), transform.rotation);
            else if(sr.flipX)
                Instantiate(itemManager.notePrefab as GameObject, new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z), transform.rotation);
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay(){  // 공격 딜레이 코루틴
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    void Death(){   // 사망 메소드
        if(life == 0){
            // 사망 처리
            transform.position = new Vector3(savePoint.x, savePoint.y, savePoint.z);
            life = maxLife;
        }
    }    

    bool IsPlayingAnim(string animName){
        if(anim.GetCurrentAnimatorStateInfo(0).IsName(animName)){
            return true;
        }
        return false;
    }

    void PlayerAnim(){
        if((Mathf.Abs(rb.velocity.x) <= 0.01f) && Mathf.Abs(rb.velocity.y) <= 0.01f){
            MyAnimSetTrigger("Idle");
        }
        else if(Mathf.Abs(rb.velocity.x) > 0.01 && Mathf.Abs(rb.velocity.y) <= 0.01f){
            MyAnimSetTrigger("Walk");
        }
        else if(rb.velocity.y < 0 && !IsPlayingAnim("Jump")){
            MyAnimSetTrigger("Down");
        }
    }

    void MyAnimSetTrigger(string animName){
        if(!IsPlayingAnim(animName)){
            anim.SetTrigger(animName);
        }
    }

    bool PlayerFlip(){
        bool flipSprite = (sr.flipX ? (moveDir > 0f) : (moveDir < 0f));
        if(flipSprite){
            sr.flipX = !sr.flipX;
            GroundFriction();
        }
        return flipSprite;
    }

    void GroundCheck(){
        if(Physics2D.BoxCast(playerCollider[2].bounds.center, playerCollider[2].size, 0, Vector2.down, 0.01f)){
            isGround = true;
            anim.ResetTrigger("Idle");
        }
        else{
            isGround = false;
        }
    }

    void GroundFriction(){
        if(isGround){
            if(IsPlayingAnim("Attack")){
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate + AttackSlideRate), rb.velocity.y);
            }
            else if(Mathf.Abs(moveDir) <= 0.01f){
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref refVelocity, slideRate), rb.velocity.y);
            }
        }
    }

    IEnumerator UnBeatTime(){
        int countTime = 0;
        while(countTime < 10){
            if(countTime%2 == 0)
                sr.color = new Color32(255, 255, 255, 90);
            else
                sr.color = new Color32(255, 255, 255, 180);
            
            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        sr.color = new Color32(255, 255, 255, 255);
        isUnBeatTime = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision);
        if(collision.gameObject.tag == "Background" || collision.gameObject.tag == "Platform")
            dashChance = true;
        if(collision.gameObject.tag == "Check"){
            savePoint = transform.position;
            Debug.Log("세이브!");
        }
        if(collision.gameObject.tag == "Monster" && !collision.isTrigger){
            Vector2 attackedVelocity = Vector2.zero;
            if(collision.gameObject.transform.position.x > transform.position.x)
                attackedVelocity = new Vector2(-3f, 3f);
            else
                attackedVelocity = new Vector2(3f, 3f);
            
            rb.AddForce(attackedVelocity, ForceMode2D.Impulse);

            life--;

            if(life > 1){
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
        }

        itemManager.ItemApply(collision);

        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag;
        enemyPosition = collision.transform.position;
        stageManager.activeObject();
    }

    private void OnTriggerExit2D(Collider2D collision)  // 플레이어가 접촉 판정을 나갔을 때 실행
    {
        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag + "Out";
        enemyPosition = collision.transform.position;
        stageManager.activeObject();
    }      

    private void OnCollisionStay2D(Collision2D collision) {
        dashChance = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // if(collision.gameObject.tag == "Attack"){
        //     life -= 1;
        //     Debug.Log("플레이어 피격!");
        // }

        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag;
        enemyPosition = collision.transform.position;        
        stageManager.activeObject();  
    }

    private void OnCollisionExit2D(Collision2D collision)        // 플레이어와 접촉이 떨어졌을 시
    {
        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag + "Out";
        enemyPosition = collision.transform.position;
        stageManager.activeObject();        
    }
}
