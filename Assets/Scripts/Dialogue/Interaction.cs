using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 상호작용하는 오브젝트에 걸어야 할 스크립트

public class Interaction : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    public Dialogue[] GetDialogues()
    {
        DialogueEvent t_DialogueEvent = new DialogueEvent();
        t_DialogueEvent.dialogues = KDatabaseManager.kinstance.GetDialogues((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_DialogueEvent.dialogues[i].tf_Target = dialogueEvent.dialogues[i].tf_Target;
        }

        dialogueEvent.dialogues = t_DialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }
}
