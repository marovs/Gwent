using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttributes : MonoBehaviour
{

    public string layer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (layer != null) gameObject.layer = LayerMask.NameToLayer(layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
