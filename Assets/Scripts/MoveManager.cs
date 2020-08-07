using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveManager : MonoBehaviour
{

    EventSystem eventSystem;
    PlayerController player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void LeftDown(){
        player.inputLeft = true;
    }

    public void LeftUp(){
        player.inputLeft = false;
    }

    public void RightDown(){
        player.inputRight = true;
    }

    public void RightUp(){
        player.inputRight = false;
    }

    public void JumpBtnDown(){
        player.inputJump = true;
    }

    public void JumpBtnUp(){
        player.inputJump = false;
    }
}
