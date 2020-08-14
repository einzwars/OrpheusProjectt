using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : MonoBehaviour
{
    public GameObject hadesObject;
    public GameObject bossInObject;

    public GameObject hadesNotePrefab1;
    public GameObject hadesNotePrefab2;
    public GameObject[] monsters;
    public Transform[] monstersPosition;
    public bool monsterOn;    
    public int monsterIndex;
    public float monsterCount;
    public float monsterDeathCount;
    public float[] monsterMaxCountList;
    public bool[] monsterCheckList;


    public GameObject items;
    public Transform[] itemPosition;        
    public int itemIndex;
    public bool itemOn;
    public int itemCount;


    public bool bossIn;    
    public bool[] phaseNum;


    float centerPositionX;
    float centerPositionY;
    int skillNum;
    int leach;
    int anglePer = 1;
    public bool[] skillOn;
    public float[] skillPositionXList;
    public float[] skillPositionYList;
    public float[] skillTwoPositionYList;
    public float[] skillLeachXList;
    public float[] skillLeachYList;
    public float[] skillCountList;
    public float[] skillOneMaxCountList;
    public float[] skillTwoMaxCountList;
    public float[] skillThreeMaxCountList;
    Animator animator;

    void Start()
    {
        centerPositionX = gameObject.transform.position.x;
        centerPositionY = gameObject.transform.position.y;
        animator = GameObject.Find("Hades").GetComponent<Animator>();
        StartCoroutine("SkillCount");        
    }

    private void Update() {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("AttackH") || animator.GetCurrentAnimatorStateInfo(0).IsName("AttackedH")){
            animator.SetTrigger("Idle");
        }
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
            GameObject.Find("Hades").GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(10f);
        }
        StartCoroutine("SkillCount");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Attack")
        {            
            if (phaseNum[0] == true && monsterDeathCount >= monsterMaxCountList[0])
            {
                phaseNum[0] = false;
                phaseNum[1] = true;
                monsterOn = true;
                itemOn = true;
                monsterDeathCount = 0;
                GameObject.Find("Hades").GetComponent<Animator>().SetTrigger("Attacked");
            }
            if (phaseNum[1] == true && monsterDeathCount >= monsterMaxCountList[1])
            {
                phaseNum[1] = false;
                phaseNum[2] = true;
                monsterOn = true;
                itemOn = true;
                monsterDeathCount = 0;
                GameObject.Find("Hades").GetComponent<Animator>().SetTrigger("Attacked");
            }
            if (phaseNum[2] == true && monsterDeathCount >= monsterMaxCountList[2])
            {
                phaseNum[2] = false;
                monsterDeathCount = 0;
                GameObject.Find("Hades").GetComponent<Animator>().SetTrigger("Attacked");
                GameObject.Find("KillHades").transform.position = GameObject.Find("Player").transform.position;
            }
        }
    }

    void FixedUpdate()
    {  
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
                    shoot.transform.localScale = new Vector3(1, 1, 1);
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
            if(monsterOn)
            {
                if (monsterCount < monsterMaxCountList[0])
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 16);
                    } while (monsterCheckList[span] == true);
                    monsterCheckList[span] = true;
                    monsterIndex = span;
                    GameObject monster = Instantiate(monsters[monsterIndex]) as GameObject;
                    monster.transform.position = monstersPosition[monsterIndex].transform.position;
                    monsterCount++;
                }
                if (monsterCount == monsterMaxCountList[0])
                {
                    for (int i = 0; i < 16; i++)
                    {
                        monsterCheckList[i] = false;
                    }
                    monsterCount = 0;
                    monsterOn = false;
                }
            }
            if (itemOn)
            {
                if (monsterDeathCount == monsterMaxCountList[0] / 2 && itemCount < 1)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;                    
                    GameObject item = Instantiate(items) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[0] && itemCount < 2)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;
                    GameObject item = Instantiate(items) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[0])
                {
                    itemOn = false;
                    itemCount = 0;
                }
            }
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
                    shoot.transform.localScale = new Vector3(1, 1, 1);
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
            if (monsterOn)
            {
                if (monsterCount < monsterMaxCountList[1])
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 16);
                    } while (monsterCheckList[span] == true) ;
                    monsterCheckList[span] = true;
                    monsterIndex = span;
                    GameObject monster = Instantiate(monsters[monsterIndex]) as GameObject;
                    monster.transform.position = monstersPosition[monsterIndex].transform.position;
                    monsterCount++;
                }
                if (monsterCount == monsterMaxCountList[1])
                {
                    for (int i = 0; i < 16; i++)
                    {
                        monsterCheckList[i] = false;
                    }
                    monsterCount = 0;
                    monsterOn = false;
                }
            }
            if (itemOn)
            {
                if (monsterDeathCount == monsterMaxCountList[1] / 2 && itemCount < 1)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;
                    GameObject item = Instantiate(items.gameObject) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[1] && itemCount < 2)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;
                    GameObject item = Instantiate(items.gameObject) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[1])
                {
                    itemCount = 0;
                    itemOn = false;
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
                if (skillCountList[2] < skillThreeMaxCountList[2])                {
                    GameObject shoot = Instantiate(hadesNotePrefab1) as GameObject;
                    shoot.transform.position = new Vector3(centerPositionX, centerPositionY + skillPositionYList[2], 0);
                    shoot.transform.localScale = new Vector3(1, 1, 1);
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
            if (monsterOn)
            {
                if (monsterCount > monsterMaxCountList[2])
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 16);
                    } while (monsterCheckList[span] == true);
                    monsterCheckList[span] = true;
                    monsterIndex = span;
                    GameObject monster = Instantiate(monsters[monsterIndex]) as GameObject;
                    monster.transform.position = monstersPosition[monsterIndex].transform.position;
                    monsterCount++;
                }
                if (monsterCount == monsterMaxCountList[2])
                {
                    for (int i = 0; i < 16; i++)
                    {
                        monsterCheckList[i] = false;
                    }
                    monsterCount = 0;
                    monsterOn = false;
                }
            }
            if (itemOn)
            {
                if (monsterDeathCount == monsterMaxCountList[2] / 2 && itemCount < 1)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;
                    GameObject item = Instantiate(items.gameObject) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[2] && itemCount < 2)
                {
                    int span = 0;
                    do
                    {
                        span = (int)Random.Range(0, 5);
                    } while (span == itemIndex);
                    itemIndex = span;
                    GameObject item = Instantiate(items.gameObject) as GameObject;
                    item.transform.position = itemPosition[itemIndex].transform.position;
                    itemCount++;
                }
                if (monsterDeathCount == monsterMaxCountList[2])
                {
                    itemOn = false;
                    itemCount = 0;
                }
            }
        }
    }
}
    

