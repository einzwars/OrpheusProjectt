using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject buttonLeft;
    [SerializeField] GameObject buttonRight;
    [SerializeField] GameObject buttonJump;

    GameObject checkObj;
    bool isContact = false;
    public static bool isInteract = false;
    bool startScene = false;

    DialogueManager theDM;
    
    void Start()
    {
        player = GameObject.Find("Player");
        theDM = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    theDM.ShowDialogue(checkObj.transform.GetComponent<Interaction>().GetDialogues());
                    isInteract = true;
                }
            }
            
        }
        
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))      // 대사 상호작용하는 오브젝트 태그
        {
            isContact = true;
            checkObj = collision.gameObject;
        }
        else
        {
            isContact = false;
            checkObj = null;
        }
    }

    public void SettingUI(bool p_flag)
    {
        buttonLeft.SetActive(p_flag);
        buttonRight.SetActive(p_flag);
        buttonJump.SetActive(p_flag);
        isInteract = !p_flag;
    }
}
