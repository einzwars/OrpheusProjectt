using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPadObject : MonoBehaviour
{    
    StageManager stageManager;
    Animator jumpPadAni;
    public bool jumpIn;
    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        jumpPadAni = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stageManager.player.rb.velocity.y == 0)
        {
            jumpIn = false;
        }
    }

    public void Jump()
    {
        if(!jumpIn)
        {
            // jumpPadAni.ResetTrigger("JumpIn");
            jumpPadAni.SetTrigger("JumpIn");
            stageManager.player.rb.AddForce(transform.up * 300);
            stageManager.player.rb.velocity = new Vector2(0, 0);
            jumpIn = true;
        }
    }
}
