using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArmCamera : BasicCamera
{

    public Transform Vertical;
    public Transform Horizontal;

    [Range(0f, 1f)]
    public float AxisX = 0f;
    public float AxisY = 0f;

    private void Awake()
    {
        base.targetCam = GetComponentInChildren<Camera>();
        base.CamTranform = this.transform;

        Vertical = transform.parent.GetComponent<Transform>();

        Horizontal = GetComponent<Transform>();
       

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SightRotate(float angle)
    {

    }


}
