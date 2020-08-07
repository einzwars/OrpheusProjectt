using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    float speed = 0.001f;
    float lifeTime = 3;
    Rigidbody2D noteRigid;
    PlayerController player;

    void Start()
    {
        noteRigid = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if(player.sr.flipX == false){
            noteRigid.AddForce(transform.right * speed, ForceMode2D.Impulse);   // 오른쪽
        }
        else if(player.sr.flipX == true){
            noteRigid.AddForce(transform.right * -speed, ForceMode2D.Impulse);  // 왼쪽
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "Trigger")
            Destroy(gameObject);
    }
}
