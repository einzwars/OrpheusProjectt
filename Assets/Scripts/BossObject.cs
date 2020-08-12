using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : MonoBehaviour
{
    public GameObject hadesNotePrefab1;
    public GameObject hadesNotePrefab2;
    public GameObject[] monsters;
    public Transform[] monstersPosition;
    public bool[] monsterDeath;
    public bool[] monsterRespawn;
    public int monsterIndex;
    int skillNum;
    int leach;
    int anglePer = 1;
    public bool[] skillOn;

    public GameObject[] items;
    public Transform[] itemPosition;
    public bool[] itemRespawn;
    public int itemIndex;    
    public bool bossIn;
    public int phaseIndex;
    public bool[] phaseNum;
    float centerPositionX;
    float centerPositionY;
    public float[] skillPositionXList;
    public float[] skillPositionYList;
    public float[] skillTwoPositionYList;
    public float[] skillLeachXList;
    public float[] skillLeachYList;
    public float[] skillCountList;
    public float[] skillOneMaxCountList;
    public float[] skillTwoMaxCountList;
    public float[] skillThreeMaxCountList;

    void Start()
    {
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        StartCoroutine("SkillCount");
        StartCoroutine("MonsterCount");
    }

    IEnumerator SkillCount()
    {
        yield return new WaitForSeconds(0.01f);
        if (bossIn)
        {            
            skillNum = Random.Range(0, 3);
            if(skillNum == 0)
            {                
                skillOn[0] = true;
            }
            if (skillNum == 1)
            {
                leach = (int)Random.Range(0, 4);
                skillOn[1] = true;
            }
            if (skillNum == 2)
            {
                skillOn[2] = true;
            }            
            yield return new WaitForSeconds(10f);
        }
        StartCoroutine("SkillCount");
    }

    IEnumerator MonsterCount()
    {
        yield return new WaitForSeconds(0.01f);
        if (bossIn)
        {
            if(monsterDeath[monsterIndex])
            {
                yield return new WaitForSeconds(5f);
                monsterRespawn[monsterIndex] = true;
            }            
        }
        StartCoroutine("MonsterCount");        
    }

    void FixedUpdate()
    {        
        if(monsterRespawn[monsterIndex])
        {
            GameObject monster = Instantiate(monsters[monsterIndex]) as GameObject;
            monster.transform.position = monstersPosition[monsterIndex].transform.position;
            monsterDeath[monsterIndex] = false;
            monsterRespawn[monsterIndex] = false;
        }
        if (phaseNum[0] == true)
        {
             if (skillOn[0])
             {
                 if (skillCountList[0] < skillOneMaxCountList[0])
                 {
                    leach = (int)Random.Range(centerPositionX - skillLeachXList[0], centerPositionX + skillLeachXList[0] + 1);
                    GameObject shoot = Instantiate(hadesNotePrefab2) as GameObject;
                    shoot.transform.position = new Vector3(leach, centerPositionY + skillPositionYList[0], 0);
                    shoot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    skillCountList[0]++;
                 }
                 if (skillCountList[0] == skillOneMaxCountList[0])
                 {                        
                        skillCountList[0] = 0;
                        skillOn[0] = false;
                 }
             }
             if (skillOn[1])
             {
                if (skillCountList[1] < skillTwoMaxCountList[0])
                {

                    GameObject shoot1 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot2 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot1.transform.localScale = new Vector3(3, 3, 3);
                    shoot2.transform.localScale = new Vector3(3, 3, 3);
                    shoot1.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                }
                else if (skillCountList[1] == skillTwoMaxCountList[0])
                {                        
                    skillCountList[1] = 0;
                    skillOn[1] = false;
                }
             }

             if (skillOn[2])
             {
                  if (skillCountList[2] < skillThreeMaxCountList[0])
                  {
                      GameObject shoot = Instantiate(hadesNotePrefab1) as GameObject;
                      shoot.transform.position = new Vector3(centerPositionX, centerPositionY + skillPositionYList[2], 0);
                      shoot.transform.rotation = Quaternion.Euler(0, 0, anglePer * 30);
                      anglePer++;
                      skillCountList[2]++;
                  }
                  else if (skillCountList[2] == skillThreeMaxCountList[0])
                  {
                      skillCountList[2] = 0;
                      skillOn[2] = false;
                      anglePer = 1;
                  }
             }
                //if (monTime > 30f)
                //{
                //monTime = 0;
                //}
                //if (itemCount >= 5f)
                //{
                //itemCount = 0;
                //}
                //if (attCount >= 10f)
                //{
                //
                //}
        }
        
        else if (phaseNum[1] == true)
        {

            if (skillOn[0])
            {

                if (skillCountList[0] < skillOneMaxCountList[1])
                {                    
                    GameObject shoot = Instantiate(hadesNotePrefab2) as GameObject;
                    leach = (int)Random.Range(centerPositionX - skillLeachXList[0], centerPositionX + skillLeachXList[0] + 1);
                    shoot.transform.position = new Vector3(leach, centerPositionY + skillPositionYList[0], 0);
                    shoot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    skillCountList[0]++;
                }
                if (skillCountList[0] == skillOneMaxCountList[1])
                {
                    skillCountList[0] = 0;
                    skillOn[0] = false;
                }

            }

            if (skillOn[1])
            {
                if (skillCountList[1] < skillTwoMaxCountList[1])
                {
                    int leach2 = 0;
                    do
                    {
                        leach2 = (int)Random.Range(0, 4);
                    }while (leach == leach2);                        
                    GameObject shoot1 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot2 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot1.transform.localScale = new Vector3(3, 3, 3);
                    shoot2.transform.localScale = new Vector3(3, 3, 3);
                    shoot1.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                    GameObject shoot3 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot4 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot3.transform.localScale = new Vector3(3, 3, 3);
                    shoot4.transform.localScale = new Vector3(3, 3, 3);
                    shoot3.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach2], 0);
                    shoot4.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach2], 0);
                    shoot4.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                }
                    else if (skillCountList[1] == skillTwoMaxCountList[1])
                    {                        
                        skillCountList[1] = 0;
                        skillOn[1] = false;
                    }
            }
            if (skillOn[2])
            {
                if (skillCountList[2] < skillThreeMaxCountList[1])
                {
                    GameObject shoot = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot.transform.position = new Vector3(centerPositionX, centerPositionY + skillPositionYList[2], 0);
                    shoot.transform.rotation = Quaternion.Euler(0, 0, anglePer * 18);
                    anglePer++;
                    skillCountList[2]++;
                }
                else if (skillCountList[2] == skillThreeMaxCountList[1])
                {
                    skillCountList[2] = 0;
                    skillOn[2] = false;
                    anglePer = 1;
                }


            }
        }

        if (phaseNum[2] == true)
        {
            if (skillOn[0])
            {
                if (skillCountList[0] < skillOneMaxCountList[2])
                {
                    GameObject shoot = Instantiate(hadesNotePrefab2) as GameObject;
                    leach = (int)Random.Range(centerPositionX - skillLeachXList[0], centerPositionX + skillLeachXList[0] + 1);
                    shoot.transform.position = new Vector3(leach, centerPositionY + skillPositionYList[0], 0);
                    shoot.transform.rotation = Quaternion.Euler(0, 0, 0);
                    skillCountList[0]++;
                }

                if (skillCountList[0] == skillOneMaxCountList[2])
                {                        
                        skillCountList[0] = 0;
                        skillOn[0] = false;
                }
            }
            if (skillOn[1])
            {
                if (skillCountList[1] < skillTwoMaxCountList[2])
                {
                    int leach2 = 0;
                    do
                    {
                        leach2 = (int)Random.Range(0, 4);
                    } while (leach == leach2);
                    int leach3 = 0;
                    do
                    {
                        leach3 = (int)Random.Range(0, 4);
                    } while (leach2 == leach3 || leach == leach3);
                    GameObject shoot1 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot2 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot1.transform.localScale = new Vector3(3, 3, 3);
                    shoot2.transform.localScale = new Vector3(3, 3, 3);
                    shoot1.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach], 0);
                    shoot2.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                    GameObject shoot3 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot4 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot3.transform.localScale = new Vector3(3, 3, 3);
                    shoot4.transform.localScale = new Vector3(3, 3, 3);
                    shoot3.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach2], 0);
                    shoot4.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach2], 0);
                    shoot4.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                    GameObject shoot5 = Instantiate(hadesNotePrefab1) as GameObject;
                    GameObject shoot6 = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot5.transform.localScale = new Vector3(3, 3, 3);
                    shoot6.transform.localScale = new Vector3(3, 3, 3);
                    shoot5.transform.position = new Vector3(centerPositionX + skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach3], 0);
                    shoot6.transform.position = new Vector3(centerPositionX - skillPositionXList[1], centerPositionY + skillTwoPositionYList[leach3], 0);
                    shoot6.transform.rotation = Quaternion.Euler(0, 180, 0);
                    skillCountList[1]++;
                    skillCountList[1]++;
                }
                else if (skillCountList[1] == skillTwoMaxCountList[2])
                {
                    skillCountList[1] = 0;
                    skillOn[1] = false;
                }
            }
            if (skillOn[2])
                {
                if (skillCountList[2] < skillThreeMaxCountList[2])
                {
                    GameObject shoot = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot.transform.position = new Vector3(centerPositionX, centerPositionY + skillPositionYList[2], 0);
                    shoot.transform.rotation = Quaternion.Euler(0, 0, anglePer * 12.857f);
                    anglePer++;
                    skillCountList[2]++;
                }
                else if (skillCountList[2] == skillThreeMaxCountList[2])
                {
                    skillCountList[2] = 0;
                    skillOn[2] = false;
                    anglePer = 1;
                }
            }
        }
    }
}
    

