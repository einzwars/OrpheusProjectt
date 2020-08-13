using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    PlayerController player;
    public GameObject hellKeeperPrefab;
    public GameObject exclamationMarkPrefab;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void ScenarioProduction(Collider2D collision){
        if(collision.name == "MeetHellkeeper"){
            Instantiate(hellKeeperPrefab as GameObject, new Vector3(16.31f, -9.95f, 0), transform.rotation);
        }
        if(collision.name == "HadesDdiyong"){
            Instantiate(exclamationMarkPrefab as GameObject, new Vector3(45f, -8f, 0), transform.rotation);
            Destroy(GameObject.Find("ExclamationMark(Clone)"), 1.5f);
        }
        if(collision.name == "KimDdiyong"){
            Instantiate(exclamationMarkPrefab as GameObject, new Vector3(45f, -8f, 0), transform.rotation);
            Destroy(GameObject.Find("ExclamationMark(Clone)"), 1.5f);
        }
        if(collision.name == "SadSong"){
            //  슬픈 노래 연출
        }
    }
}
