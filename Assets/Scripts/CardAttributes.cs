using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardAttributes : MonoBehaviour
{
    public byte id;
    public string lane;
    public int power;

    private TextMeshProUGUI _powerText;
    
    private void Awake()
    {
        if (lane != null) gameObject.layer = LayerMask.NameToLayer(lane);

        TextMeshProUGUI[] textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            if (textComponent.name.Equals("PowerText")) _powerText = textComponent;
        }

        try
        {
            _powerText.text = power.ToString();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
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
