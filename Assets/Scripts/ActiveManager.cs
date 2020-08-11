using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    float posX;
    float posY;
    PlayerController player;

    void Update() {
        ObjRegenerate();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        posX = this.gameObject.transform.position.x;
        posY = this.gameObject.transform.position.y;       
    }

    public void ObjRegenerate()
    {
        if(player.checkTrigger){
            this.gameObject.transform.position = new Vector3(posX, posY, 0);
            if(this.gameObject.tag == "Fall"){
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }                
            else if(this.gameObject.tag == "Death"){
                Debug.Log("떨어지는 함정 재배치!");
                this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                this.gameObject.GetComponent<Renderer>().enabled = true;           
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }
}
