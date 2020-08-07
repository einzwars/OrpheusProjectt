using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadObject : MonoBehaviour
{    
    public StageManager stageManager;
    Animator jumpPadAni;
    bool jumpIn;
    // Start is called before the first frame update
    void Start()
    {
        jumpPadAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stageManager.jumpCheck)  // 플레이어가 점프대를 올라 변수가 확인되었을 때
        {
            if (jumpIn == false)
            {
                jumpPadAni.SetTrigger("JumpIn");
                stageManager.player.rb.AddForce(transform.up * 500);
                stageManager.player.rb.gravityScale = 0;
                stageManager.player.rb.velocity = new Vector2(0, 0);
                jumpIn = true;
            }
            else if (stageManager.player.transform.position.y > (stageManager.jumpPadFirst + 2) && jumpIn == true)    // 자신의 위치에서 6 높은 곳까지 이동할 수 있도록 한다
            {
                jumpPadAni.SetTrigger("JumpOut");
                stageManager.player.rb.gravityScale = 2;
                jumpIn = false;
                stageManager.jumpCheck = false;                                  // 이후 변수를 false 로 변경
            }
        }
    }






}
