using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeProperties : MonoBehaviour
{

    public bool isBase = false;
    public int numAttached = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(this.transform.parent == null)
        {
            isBase = true;
            numAttached = transform.childCount;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
