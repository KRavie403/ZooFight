using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedBackgroundPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        transform.localPosition = initialPosition;
    }
}
