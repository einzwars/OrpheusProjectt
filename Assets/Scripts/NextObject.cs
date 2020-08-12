using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextObject : MonoBehaviour
{
    public int loadSceneNum;
    PlayerController player;
    Transform startPosition;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void NextStage()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
