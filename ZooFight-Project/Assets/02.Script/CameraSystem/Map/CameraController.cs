using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rect renderRect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);

    private void Update()
    {
        Camera.main.rect = renderRect;
    }
}
