using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float speed = 0.01f;
    Vector3 dir = new Vector3(0, 1, 0); //이동 속도 방향

    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(dir * 360.0f * Time.deltaTime *  speed);
    }
}
