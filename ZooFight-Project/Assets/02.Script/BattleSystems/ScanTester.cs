using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanTester : MonoBehaviour,IHitScanner
{
    Component IHitScanner.myComp => this as Component;

    Component[] IHitScanner.myTargets => targets;

    Component[] targets = new Component[0];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IHitScanner.AddTarget(List<Component> target)
    {
        targets = target.ToArray();
    }

    void IHitScanner.Hit()
    {
        Debug.Log("Hit");
        transform.Translate(Vector3.forward);
    }

}
