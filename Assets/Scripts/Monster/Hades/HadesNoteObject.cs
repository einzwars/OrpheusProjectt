using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesNoteObject : MonoBehaviour
{
    public float noteSpeed = 0.1f;
    public bool yellowNote;
    // Update is called once per frame
    void Update()
    {
        if(yellowNote)
        {
            transform.Translate(0, -noteSpeed, 0);
            Destroy(this.gameObject, 10);
        }
        if (!yellowNote)
        {
            transform.Translate(-noteSpeed, 0, 0);
            Destroy(this.gameObject, 10);
        }
    }
}
