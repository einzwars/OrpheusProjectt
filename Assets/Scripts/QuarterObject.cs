using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterObject : MonoBehaviour
{
    public StageManager stageManager;

    public void QuaterPointUp()
    {
        stageManager.player.quarterPoint = +1;        
    }
}
