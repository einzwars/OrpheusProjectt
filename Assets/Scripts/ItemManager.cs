﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // 아이템 변경 필드
    public GameObject fireballPrefab;
    public GameObject blueballPrefab;
    public GameObject notePrefab;
    public GameObject threeBallPrefab;
    public GameObject pomulPrefab;
    public bool basicAtk;
    public bool threeBall;
    public bool pomul;
    // 탄창 UI
    int tripleMagazine = 50;
    int pomulMagazine = 30;
    int LeftBullet;
    Text LeftBulletText;
    Image bulletImage;

    void Start()
    {
        LeftBulletText = GameObject.Find("LeftBullet").GetComponent<Text>();
        bulletImage = GameObject.Find("BulletMagazine").GetComponent<Image>();
    }

    void Update()
    {
        this.LeftBulletText.text = this.LeftBullet.ToString();
        if (LeftBullet == 0)
        {
            threeBall = false;
            pomul = false;
            basicAtk = true;
        }
    }

    public void ConsumeBullet() {
            if (LeftBullet > 0) { // 남은 총알이 0보다 많을 때만 탄창이 줄어듬
            this.LeftBullet -= 1;
        }
    }

    public void ItemApply(Collider2D collision){
        if (collision.gameObject.name == "GatlingItem") {   // 연사 아이템
            Destroy(collision.gameObject);
            // LeftBullet = gatlingMagazine;
            threeBall = false;
            pomul = false;
            basicAtk = false;
        }
        if(collision.gameObject.name == "GuideItem") {  // 유도 아이템
            Destroy(collision.gameObject);
            // LeftBullet = guideMagazine;
            threeBall = false;
            pomul = false;
            basicAtk = false;
        }
        if(collision.gameObject.name == "TripleItem"){  // 삼연발 아이템
            Destroy(collision.gameObject);
            LeftBullet = tripleMagazine;
            threeBall = true;
            pomul = false;
            basicAtk = false;
            this.bulletImage.sprite = Resources.Load("Item/TripleItem", typeof(Sprite)) as Sprite;
        }
        if(collision.gameObject.name == "PomulItem"){   // 곡사 아이템
            Destroy(collision.gameObject);
            LeftBullet = pomulMagazine;
            threeBall = false;
            pomul = true;
            basicAtk = false;
            this.bulletImage.sprite = Resources.Load("Item/PomulItem", typeof(Sprite)) as Sprite;
        }
    }
}