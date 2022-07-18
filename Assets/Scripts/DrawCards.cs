using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{

    public GameObject[] cards;
    public GameObject handArea;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(cards[Random.Range(0, cards.Length)], handArea.transform, false);
        }
    }
}
