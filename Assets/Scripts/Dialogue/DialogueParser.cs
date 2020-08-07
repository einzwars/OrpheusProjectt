using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        // 대화 리스트 생성
        List<Dialogue> dialogueList = new List<Dialogue>();
        // csv파일 가져옴
        TextAsset csvData = Resources.Load(_CSVFileName) as TextAsset;

        // 엔터 기준으로 쪼갬
        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        // csv파일에서 두번째 줄부터 대사가 쓰여있으므로 int i는 1부터 시작
        {
            // ,를 기준으로 ID, 이름, 대사를 각각 쪼개줌
            string[] row = data[i].Split(new char[] { '|' });
            
            // 대사 리스트 생성
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[1];

            List<string> contextList = new List<string>();
            
            // contextList에다가 row[2]을 추가
            // 이거를 data[1]부터 쭈욱 돌려줌
            // dialogue.name이 같을 때 dialogue.context만 돌려주고 싶음
            // row[0]이 비어 있으면 row[1]은 그대로, row[2]만 넘겨줌
            // row[0]이 +1되면 row[1]도 넘기고, row[2]도 넘겨줌
            
            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[]{'|'});
                }
                else
                {
                    break;
                }
                
            }while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();

            // 같은 이름에 맞는 대사들이 한 세트로 들어감
            // 즉 같은 캐릭터가 말하면 한 개로 들어감
            dialogueList.Add(dialogue);

        }


        return dialogueList.ToArray();
    }
    

}
