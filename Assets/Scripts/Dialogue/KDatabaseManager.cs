using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDatabaseManager : MonoBehaviour
{
    public static KDatabaseManager kinstance;

    [SerializeField] public string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    void Awake()
    {
        
        if (kinstance == null)
        {
            kinstance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            
            for (int i = 0; i < dialogues.Length; i++)
            {
                // dialogueDic = 1번에 한 캐릭터의 대사 한 세트를 집어넣어줌
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            
            isFinish = true;
        }
    }

    public Dialogue[] GetDialogues(int _StartNum, int _EndNum)
    {
        // _StartNum부터 _EndNum까지의 dialogues를 불러옴
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        // 한 세트의 대화를 만듦
        return dialogueList.ToArray();
    }
}
