using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Off : MonoBehaviour
{
    public GameObject Obj_Interaction;
    GameObject player;
    PlayerController playerController;
    InteractionController theIC;


    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        theIC = player.GetComponent<InteractionController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && gameObject.tag == "Interaction")
        {
            Obj_Interaction.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //
    }
}
