using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text nameText;
    public Text talkText;    
    public FadeController fader;

    Dialogue[] dialogues;

    InteractionController theIC;
    // CameraController theCam;

    public bool isAction = false; // 대화 중일 경우 true
    bool isNext = false; // 특정 키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대사 카운트
    GameObject player;
    CloudObject cloudObject;
    float timer = 0;
    bool emovetrigger;

    void Start()
    {
        player = GameObject.Find("Player");
        fader = GameObject.Find("FadeController").GetComponent<FadeController>();
        theIC = FindObjectOfType<InteractionController>();
        // theCam = FindObjectOfType<CameraController>();
        if(SceneManager.GetActiveScene().name == "Stage5 Scenario"){
            cloudObject = GameObject.Find("5StageClouds").GetComponent<CloudObject>();
        }
    }


    void Update()
    {
        
        if (isAction)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNext = false;
                    talkText.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWritter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            // theCam.CameraTargetting(dialogues[lineCount].tf_Target);
                            StartCoroutine(TypeWritter());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
        if(emovetrigger && timer <= 2.5){
            timer += Time.deltaTime;
            GameObject emprydike = GameObject.Find("Emprydike");
            Animator empydikeAnim = GameObject.Find("Emprydike").GetComponent<Animator>();
            empydikeAnim.SetTrigger("Walk");
            emprydike.transform.Translate(transform.right * 0.01f);
        }
        else if(timer >= 2.5){
            GameObject emprydike = GameObject.Find("Emprydike");
            timer = 0;
            emovetrigger = false;
            fader.StartFade();
            Invoke("Prologue4", 1.2f);
        }
    }
    
    void Prologue4(){
        GameObject.Find("Prologue4").transform.position = player.transform.position;
    }

    IEnumerator TypeWritter()
    {
        // talkPanel.SetActive(true);

        string text_dialogue = dialogues[lineCount].contexts[contextCount];

        nameText.text = dialogues[lineCount].name;
        for (int i = 0; i < text_dialogue.Length; i++)
        {
            talkText.text += text_dialogue[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
    }
    

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isAction = true;
        nameText.text = "";
        talkText.text = "";
        dialogues = p_dialogues;

        theIC.SettingUI(false);
        talkPanel.SetActive(true);
        
        // theCam.CameraTargetting(dialogues[lineCount].tf_Target);
        StartCoroutine(TypeWritter());
    }
    
    void EndDialogue()
    {
        isAction = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        talkPanel.SetActive(false);
        Production();
    }

    void Production(){
        if (theIC.checkObj.name == "Letter")
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        if(theIC.checkObj.name == "MeetHellkeeper"){
            Destroy(GameObject.Find("HellKeeper(Clone)"));
        }
        if(theIC.checkObj.name == "MeetHades"){
            GameObject.Find("HadesDdiyong").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "HadesDdiyong"){
            GameObject.Find("KimDdiyong").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "KimDdiyong"){
            GameObject.Find("SadSong").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "SadSong"){
            GameObject.Find("Hades").transform.position = new Vector3(45, -9, 0);
            BossObject bossObject = GameObject.Find("HadesObject").GetComponent<BossObject>();
            bossObject.bossIn = true;
            bossObject.phaseNum[0] = true;
            bossObject.monsterOn = true;
            bossObject.itemOn = true;   
        }
        if(theIC.checkObj.name == "Cloud"){
            cloudObject.CloudStart();
        }
        if(theIC.checkObj.name == "Prologue1"){
            GameObject.Find("Prologue2").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Prologue1"){
            Destroy(GameObject.Find("Prologue2Image(Clone)"));
        }
        if(theIC.checkObj.name == "Prologue2"){
            GameObject.Find("ScenarioImage").SetActive(false);
            GameObject.Find("Panel").SetActive(false);
            GameObject.Find("Prologue3").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Prologue3"){
            emovetrigger = true;
        }
        if(theIC.checkObj.name == "Prologue4"){
            GameObject.Find("Prologue5").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Prologue5"){
            GameObject.Find("Prologue6").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Prologue6"){
            fader.StartFade();
            Destroy(GameObject.Find("Emprydike"));
            Destroy(GameObject.Find("WhiteSnake"));     
            Invoke("Prologue7", 0.5f);
        }
        if(theIC.checkObj.name == "Prologue7"){
            fader.StartFade();
            Invoke("StageSelect", 1.0f);
        }
        if(theIC.checkObj.name == "Ending1"){
            GameObject.Find("Ending2").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Ending2"){
            GameObject.Find("GamePlayUI").transform.Find("ScenarioImage").gameObject.SetActive(false);
            GameObject.Find("GamePlayUI").transform.Find("Panel").gameObject.SetActive(false);
            GameObject.Find("Ending3").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Ending3"){
            GameObject.Find("Ending4").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "Ending4"){
            fader.StartFade();
            Invoke("StageSelect", 1.0f);
        }
        if(theIC.checkObj.name == "BadEnding1"){
            GameObject.Find("BadEnding2").transform.position = player.transform.position;
        }
        if(theIC.checkObj.name == "BadEnding2"){
            fader.StartFade();
            Invoke("StageSelect", 1.0f);
        }
    }

    void StageSelect(){
        SceneManager.LoadScene("StageSelectScene");
    }

    void Prologue7(){
        GameObject.Find("Prologue7").transform.position = player.transform.position;
    }
}
