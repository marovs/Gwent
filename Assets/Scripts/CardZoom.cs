using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{

    public GameObject Canvas;
    private GameObject zoomCard;

    private void Awake()
    {
        Canvas = GameObject.Find("MainCanvas");
    }

    public void OnClickEnter()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        zoomCard = Instantiate(gameObject, Canvas.transform, false);
        zoomCard.transform.position = new Vector3(worldPoint.x, worldPoint.y + 2f, 0);
        zoomCard.transform.localScale = new Vector3(2f, 2f, 1f);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");
        
        RectTransform cardRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform rectTransform = zoomCard.GetComponent<RectTransform>();
        Rect rect = cardRectTransform.rect;
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }

    public void OnClickExit()
    {
        Destroy(zoomCard);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
