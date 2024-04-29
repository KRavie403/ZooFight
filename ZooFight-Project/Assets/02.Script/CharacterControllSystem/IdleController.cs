using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleController : MonoBehaviour
{
    public Animator myAnim = null;

    public void CheckReady()
    {
        myAnim.SetBool("isReady", true);
    }
}
