using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttributes : MonoBehaviour
{

    public string lane;
    
    private void Awake()
    {
        if (lane != null) gameObject.layer = LayerMask.NameToLayer(lane);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
