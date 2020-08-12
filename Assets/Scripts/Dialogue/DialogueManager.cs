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

    Dialogue[] dialogues;

    InteractionController theIC;
    // CameraController theCam;

    public bool isAction = false; // 대화 중일 경우 true
    bool isNext = false; // 특정 키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대사 카운트

    void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        // theCam = FindObjectOfType<CameraController>();
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
        if (theIC.checkObj.name == "Letter")
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
    
}
