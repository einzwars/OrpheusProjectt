using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject buttonLeft;
    [SerializeField] GameObject buttonRight;
    [SerializeField] GameObject buttonJump;
    [SerializeField] GameObject buttonAttack;


    public GameObject checkObj;
    bool isContact = false;
    public static bool isInteract = false;
    bool startScene = false;

    GameObject dialogueManager;
    DialogueManager theDM;
    ProductionManager productionManager;
    
    void Start()
    {
        player = GameObject.Find("Player");
        productionManager = GameObject.Find("DialogueManager").GetComponent<ProductionManager>();
        if(SceneManager.GetActiveScene().name == "Prologue")
            player.transform.position = new Vector3(-7.53f, -4.239f, 0);
        dialogueManager = GameObject.Find("DialogueManager");
        theDM = dialogueManager.GetComponent<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interaction"))      // 대사 상호작용하는 오브젝트 태그
        {
            isContact = true;
            checkObj = collision.gameObject;
            if(!isInteract)
            {
                if (isContact)
                {
                    theDM.ShowDialogue(checkObj.transform.GetComponent<Interaction>().GetDialogues());
                    isInteract = true;
                }
            }
        }
        
        if(SceneManager.GetActiveScene().name == "Stage4 Scenario" || SceneManager.GetActiveScene().name == "Prologue" || SceneManager.GetActiveScene().name == "Ending" || SceneManager.GetActiveScene().name == "BadEnding")
            productionManager.ScenarioProduction(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isContact = false;
        // checkObj = null;
    }

    public void SettingUI(bool p_flag)
    {
        buttonLeft.SetActive(p_flag);
        buttonRight.SetActive(p_flag);
        buttonJump.SetActive(p_flag);
        buttonAttack.SetActive(p_flag);
        isInteract = !p_flag;
    }
}