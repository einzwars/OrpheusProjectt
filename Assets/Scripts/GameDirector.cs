using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public PlayerController player;
    Rigidbody2D playerRigid2D;  // Rigidbody2D 컴포넌트를 참조하기 위한 변수    



    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigid2D = player.GetComponent<Rigidbody2D>();
    }

    public void PlayerReposition()  // 플레이어를 스테이지 출발지로 이동시키는 메소드
    {
        player.transform.position = new Vector3(0, 0, 0);  // 플레이어의 위치를 출발지로 이동
        VelocityZero();                             // 플레이어 벨로시티 값 초기화
    }

    public void VelocityZero()   // 객체의 높이를 초기화하는 변수
    {
        playerRigid2D.velocity = Vector2.zero;  // 객체의 높이를 0 으로 초기화한다
    }
}
