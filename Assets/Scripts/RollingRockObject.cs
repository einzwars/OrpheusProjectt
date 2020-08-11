using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockObject : MonoBehaviour
{    
    Rigidbody2D rigid2D;  // Rigidbody2D 컴포넌트를 참조하기 위한 변수 
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        float nowRotationZ = transform.rotation.eulerAngles.z;
        if (rigid2D.velocity.y < 0)
        {
            if (225 <= nowRotationZ && nowRotationZ < 315)
            {
                Debug.Log("360 진입");
                rigid2D.AddForce(transform.up * -1);
            }
            if (135 <= nowRotationZ && nowRotationZ < 225)
            {
                Debug.Log("270 진입");
                rigid2D.AddForce(transform.right * 1);
            }
            if (45 <= nowRotationZ && nowRotationZ < 135)
            {
                Debug.Log("180 진입");
                rigid2D.AddForce(transform.up * 1);
            }
            if (315 <= nowRotationZ || nowRotationZ < 45)
            {
                Debug.Log("90 진입");
                rigid2D.AddForce(transform.right * -1);
            }
        }        
    }
}
