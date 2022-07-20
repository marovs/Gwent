using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttributes : MonoBehaviour
{

    public byte id;
    public string lane;
    
    private void Awake()
    {
        if (lane != null) gameObject.layer = LayerMask.NameToLayer(lane);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool Equals(CardAttributes other)
    {
        return id == other.id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(CardAttributes)) return false;
        return Equals((CardAttributes)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), id);
    }
}
