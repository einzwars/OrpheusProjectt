using System.Collections;
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
    public int jumpCount = 2;
    public bool isGrounded = false;
    public float AttackSlideRate;
    public float slideRate;
    private float refVelocity = 0.0f;
    float delayTime = 1.0f;
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
    public bool inputAttack = false;

    // 플레이어 스탯
    public int life;
    public int maxLife;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    bool isDelay = false;
    public Vector3 savePoint;  // 플레이어 세이브포인트 위치 저장

    // 맵 상호작용 필드
    public GameObject takeObject;          // 플레이어와 맞닿은 대상 대입
    public GameDirector gameDirector;      // 디렉터 대입
    public StageManager stageManager;      // 스테이지 매니저 대입
    public DamageDirector damageDirector;      // 데미지 디렉터 대입
    public Vector3 playerPos;            // 플레이어 포지션 대입
    public Collider2D playerCol; // Collider2D 컴포넌트를 참조하기 위한 변수
    public string hitObject;  // 플레이어가 맞닿은 오브젝트
    public string collisionName;
    public Vector2 enemyPosition;


    // 유니티 시스템 참조
    public Rigidbody2D rb;
    BoxCollider2D[] playerCollider;
    public SpriteRenderer sr;
    public Animator anim;
    public ItemManager itemManager;
    public EscapeManager escapeManager;
    public bool checkTrigger;
    public string playerInThis;

    void Start()
    {
        StartPosition();
        gameObject.transform.position = playerPos;
        savePoint = playerPos;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponents<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        escapeManager = GameObject.Find("GamePlayUI").transform.Find("ChoiceMenu").gameObject.GetComponent<EscapeManager>();
        jumpCount = 0;

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

    void StartPosition(){
        playerInThis = SceneManager.GetActiveScene().name;
        if(playerInThis == "Stage1 Scenario"){
            playerPos = new Vector3(-8.55f, -2.92f, 0);
        }
        else if(playerInThis == "Stage2 Scenario"){
            playerPos = new Vector3(-8.69f, -4.4f, 0);
        }
        else if(playerInThis == "Stage3 Scenario"){
            playerPos = new Vector3(-7.52f, 1.53f, 0);
        }
        else if(playerInThis == "Stage4 Scenario"){
            playerPos = new Vector3(5.45f, -12.34f, 0);
        }
        else if(playerInThis == "Stage5E"){
            playerPos = new Vector3(33.08f, -3.783f, 0);
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
        if(isGrounded){
            if(jumpCount>0){
                // rb.AddForce(transform.up*jumpPower, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                MyAnimSetTrigger("Jump");
                jumpCount--;
            }
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
        inputAttack = true;
        if(itemManager.pomul == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX)
                Instantiate(itemManager.pomulPrefab as GameObject, new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 30));
            else if(sr.flipX)
                Instantiate(itemManager.pomulPrefab as GameObject, new Vector3(transform.position.x-0.4f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, -30));
            itemManager.ConsumeBullet();
            StartCoroutine(AttackDelay());
        }
        else if(itemManager.threeBall == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX){
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, -10));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 10));
            }
            else if(sr.flipX){
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-0.4f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, -10));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-0.4f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
                Instantiate(itemManager.threeBallPrefab as GameObject, new Vector3(transform.position.x-0.4f, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 10));
            }
            itemManager.ConsumeBullet();
            StartCoroutine(AttackDelay());
        }
        else if(itemManager.basicAtk == true && !isDelay){
            anim.SetTrigger("Attack");
            isDelay = true;
            if(!sr.flipX)
                Instantiate(itemManager.notePrefab as GameObject, new Vector3(transform.position.x+0.3f, transform.position.y, transform.position.z), transform.rotation);
            else if(sr.flipX)
                Instantiate(itemManager.notePrefab as GameObject, new Vector3(transform.position.x-0.4f, transform.position.y, transform.position.z), transform.rotation);
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay(){  // 공격 딜레이 코루틴
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    void Death(){   // 사망 메소드
        if(life <= 0){
            // 사망 처리, ChoiceMenu 호출
            CheckPoint();
        }
    }

    public void CheckPoint(){
        transform.position = new Vector3(savePoint.x, savePoint.y, savePoint.z);
        life = maxLife;
        checkTrigger = true;
        Invoke("CheckTriggerFalse", 1);
    }

    public void CheckTriggerFalse(){
        checkTrigger = false;
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
        if(Physics2D.BoxCast(playerCollider[1].bounds.center, playerCollider[1].size, 0, Vector2.down, 0.01f)){
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
        // Debug.Log(collision);
        if(collision.gameObject.tag == "Check"){
            savePoint = collision.transform.position;
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
        if(collision.gameObject.tag == "cloud"){
            life = 0;
        }

        itemManager.ItemApply(collision);

        collisionName = collision.name;
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
        hitObject = collision.gameObject.tag;
        dashChance = true;
        if(hitObject == "Fall"){
            stageManager.activeObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        collisionName = collision.gameObject.name;
        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag;
        enemyPosition = collision.transform.position;        
        stageManager.activeObject();
        if(collision.gameObject.tag == "Rock"){
            life = 0;
        }
        if(collision.gameObject.tag == "Monster"){
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
    }

    private void OnCollisionExit2D(Collision2D collision)        // 플레이어와 접촉이 떨어졌을 시
    {
        takeObject = collision.gameObject;
        hitObject = collision.gameObject.tag + "Out";
        enemyPosition = collision.transform.position;
        stageManager.activeObject();        
    }
}