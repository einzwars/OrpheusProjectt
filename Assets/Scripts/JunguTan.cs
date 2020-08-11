using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunguTan : MonoBehaviour
{
    public float speed;
    PlayerController player;
    int direction;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyball", 2); // 일정시간이 지난 후 특정 함수 호출 ("실행 함수 명" , 지연 시간)
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if(player.sr.flipX == false){
            direction = 1;
        }
        else if(player.sr.flipX == true){
            direction = 2;
        }
    }
        // Update is called once per frame
        void Update()
        {
            if(direction == 1){
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else if(direction == 2){
                transform.Translate(transform.right * -speed * Time.deltaTime);
            }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroyball();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        Destroyball();
    }

    void Destroyball()
    { 
        Destroy(gameObject);
    }

 }


