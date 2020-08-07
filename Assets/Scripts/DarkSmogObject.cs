using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DarkSmogObject : MonoBehaviour
{
    public bool insideCave;
    GameObject caveCanvus;
    Image cave;
    float time = 0f;
    float fitTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        caveCanvus = GameObject.Find("CaveCanvus");
        cave = caveCanvus.transform.Find("Cave").GetComponent<Image>();        
    }

    // Update is called once per frame


    public void InsideCace()
    {
        StartCoroutine(InCave());
    }
    public void OutsideCace()
    {
        StartCoroutine(OutCave());
    }

    IEnumerator InCave()
    {
        Debug.Log("인 진입");
        cave.gameObject.SetActive(true);
        Color alpha = cave.color;        
        while(alpha.a < 0.5f)
        {
            time += Time.deltaTime / fitTime;
            alpha.a = Mathf.Lerp(0, 1, time);
            cave.color = alpha;
            yield return null;
        }
        time = 0f;
    }

    IEnumerator OutCave()
    {
        Debug.Log("아웃 진입");
        Color alpha = cave.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fitTime;
            alpha.a = Mathf.Lerp(1, 0, time);
            cave.color = alpha;
            yield return null;
        }
        time = 0f;
        cave.gameObject.SetActive(false);
    }



}
