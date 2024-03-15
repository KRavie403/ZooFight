using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour
{

    public BlockObject GrabableBlock;

    public BlockObject curGrabBlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BlockObject>() != null)
        {
            GrabableBlock = other.GetComponent<BlockObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<BlockObject>() != null)
        {
            if (other.GetComponent<BlockObject>() == GrabableBlock)
            {
                GrabableBlock = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BlockObject>() != null)
        {
            if(GrabableBlock == null)
            {
                GrabableBlock = other.GetComponent<BlockObject>();
            }
        }
    }

    //public void 

}
