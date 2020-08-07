using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
// 캐릭터의 위치를 참조해서 카메라의 위치를 갱신해준다
// https://www.youtube.com/watch?v=nhmc1z9yh0c 해당 페이지 참조

public class CameraController : MonoBehaviour{
    
    Transform playerPos;  // 플레이어의 포지션 값
    public float speed;          // 카메라의 적용할 스피드
    public Vector2 center;       // 카메라의 최대 거리를 측정할 중심점
    public Vector2 size;         // 카메라의 최대 거리를 측정할 부피
                                 // 인스펙터 창에서 화면 크기 수정 가능
    float height;                // 카메라의 촬영 세로 높이
    float width;                 // 카메라의 촬영 가로 길이

    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        height = Camera.main.orthographicSize;          // 카메라가 비추는 세로 높이 계산
        width = height * Screen.width / Screen.height;  // 카메라가 비추는 가로 길이 계산        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;           // 최대 거리 표시용 도형 색상
        Gizmos.DrawWireCube(center, size);              // 대입된 크기와 위치에 맞춰 사각형 생성
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position, Time.deltaTime * speed);        
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        // 카메라가 업데이트로 갱신된 벡터를 향해 speed에 입력된 속도로 이동한다

        float lx = size.x * 0.5f - width;
        // lx = 카메라 최대 넓이 * 0.5 - 카메라 가로 길이
        // 해당 값을 기준으로 카메라의 제한값을 생성
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        // 현재 카메라 위치(transform.position.x)를 기준으로 최소값(-lx + center.x), 최대값(lx + center.x)을 넘었는지 판단한다
        // 최댓값 초과 시 최댓값 출력, 최소값 출력 시 최소값 출력, 어떤 제한도 없을 시 변화가 없다.        

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, - 10f);
        // 이후 위의 두 값을 토대로 카메라의 위치를 변경한다

    }
}
