using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDirector : MonoBehaviour
{
    public StageManager stageManager;

    public void OnDameged(Vector2 targetPos)
    {
        if(stageManager.player.hitObject == "Peak")
        {
            stageManager.player.life = 0;
            // gameObject.layer = 9;  // 무적 스크립트
            stageManager.player.sr.color = new Color(1, 1, 1, 0.4f);   // 플레이어 스프라이트를 어둡게 변경
            int dirc = stageManager.player.transform.position.x - targetPos.x > 0 ? 1 : -1;      // 맞고 멀리 날아감
            stageManager.player.rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            Invoke("OffDameged", 3);
        }
        if (stageManager.player.hitObject == "Death")
        {
            stageManager.player.life = 0;
            // gameObject.layer = 9;  // 무적 스크립트
            stageManager.player.sr.color = new Color(1, 1, 1, 0.4f);   // 플레이어 스프라이트를 어둡게 변경
            int dirc = stageManager.player.transform.position.x - targetPos.x > 0 ? 1 : -1;      // 맞고 멀리 날아감
            stageManager.player.rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            Invoke("OffDameged", 3);
        }
    }

    void OffDameged()
    {
        // gameObject.layer = 8;  // 무적 스크립트
        stageManager.player.sr.color = new Color(1, 1, 1, 1f);    // 플레이어 색상 초기화
    }
}
