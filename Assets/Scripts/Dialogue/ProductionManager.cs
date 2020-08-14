using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProductionManager : MonoBehaviour
{
    PlayerController player;
    InteractionController interactionController;
    Image scenarioImage;
    public GameObject hellKeeperPrefab;
    public GameObject exclamationMarkPrefab;
    public GameObject soulPrefab;
    bool rmovetrigger;
    bool smovetrigger;
    float timer;
    float stimer;
    FadeController fader;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Prologue")
            interactionController = GameObject.Find("Player").GetComponent<InteractionController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        fader = GameObject.Find("FadeController").GetComponent<FadeController>();
    }

    void Update() {
        if(rmovetrigger && timer <= 2.5){
            timer += Time.deltaTime;
            GameObject player = GameObject.Find("Player");
            Animator playerAnim = GameObject.Find("Player").GetComponent<Animator>();
            Animator empydikeAnim = GameObject.Find("Emprydike").GetComponent<Animator>();
            playerAnim.SetTrigger("Walk");
            empydikeAnim.SetTrigger("Dead");
            player.transform.Translate(transform.right * 0.01f);
        }
        else{
            Animator playerAnim = GameObject.Find("Player").GetComponent<Animator>();
            rmovetrigger = false;
            timer = 0;
            playerAnim.SetTrigger("Idle");
        }

        if(smovetrigger && 0.1 <= stimer && stimer <= 2.5){
            stimer += Time.deltaTime;
            GameObject snake = GameObject.Find("WhiteSnake");
            snake.transform.Translate(transform.right * 0.002f);
        }
        else if(smovetrigger && stimer >= 2.5){
            stimer = 0;
            smovetrigger = false;
        }
        else if(smovetrigger){
            stimer += Time.deltaTime;
            GameObject.Find("WhiteSnake").GetComponent<Renderer>().enabled = true;
        }
    }

    public void ScenarioProduction(Collider2D collision){
        if(collision.name == "MeetHellkeeper"){
            Instantiate(hellKeeperPrefab as GameObject, new Vector3(16.31f, -9.95f, 0), transform.rotation);
        }
        // if(collision.name == "HadesDdiyong"){
        //     Instantiate(exclamationMarkPrefab as GameObject, new Vector3(45f, -8f, 0), transform.rotation);
        //     Destroy(GameObject.Find("ExclamationMark(Clone)"), 1.5f);
        // }
        // if(collision.name == "KimDdiyong"){
        //     Instantiate(exclamationMarkPrefab as GameObject, new Vector3(45f, -8f, 0), transform.rotation);
        //     Destroy(GameObject.Find("ExclamationMark(Clone)"), 1.5f);
        // }
        if(collision.name == "SadSong"){
            //  슬픈 노래 연출
        }
        if(collision.name == "Prologue2"){
            scenarioImage = GameObject.Find("ScenarioImage").GetComponent<Image>();
            scenarioImage.sprite = Resources.Load<Sprite>("Scenario/Scenario_3_pixel_merge") as Sprite;
        }
        if(collision.name == "Prologue4"){
            rmovetrigger = true;
        }
        if(collision.name == "Prologue5"){
            smovetrigger = true;
        }
        if(collision.name == "Prologue6"){
            Instantiate(soulPrefab as GameObject, new Vector3(GameObject.Find("Emprydike").transform.position.x, GameObject.Find("Emprydike").transform.position.y+0.1f, 0), transform.rotation);
            Destroy(GameObject.Find("Soul(Clone)"), 1.5f);
        }
        if(collision.name == "Prologue7"){
            GameObject.Find("TreeSpirit").GetComponent<Renderer>().enabled = true;
            DataController.Instance.gameData.prologueView = true;
        }
        if(collision.name == "Ending2"){
            GameObject.Find("GamePlayUI").transform.Find("ScenarioImage").gameObject.SetActive(true);
            GameObject.Find("GamePlayUI").transform.Find("Panel").gameObject.SetActive(true);
        }
        if(collision.name == "Ending4"){
            GameObject.Find("GamePlayUI").transform.Find("ScenarioImage").gameObject.SetActive(true);
            scenarioImage = GameObject.Find("ScenarioImage").GetComponent<Image>();
            scenarioImage.sprite = Resources.Load<Sprite>("Scenario/Title_StageSelect") as Sprite;
            GameObject.Find("GamePlayUI").transform.Find("Panel").gameObject.SetActive(true);
        }
        if(collision.name == "BadEnding2"){
            fader.StartFade();
            GameObject.Find("GamePlayUI").transform.Find("ScenarioImage").gameObject.SetActive(true);
            scenarioImage = GameObject.Find("ScenarioImage").GetComponent<Image>();
            scenarioImage.sprite = Resources.Load<Sprite>("Scenario/Fadein_Out") as Sprite;
        }
    }
}
