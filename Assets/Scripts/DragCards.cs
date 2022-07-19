using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCards : MonoBehaviour
{

    public GameObject canvas;
    private bool _isDragging = false;
    private bool _isDropable = false;
    private GameObject _dropZone;
    private GameObject _startParent;
    private Vector3 _startPosition;

    private void Awake()
    {
        canvas = GameObject.Find("MainCanvas");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isDragging)
        { // TODO: if player's turn
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
            transform.SetParent(canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _isDropable = true;
        _dropZone = col.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isDropable = false;
        _dropZone = null;
    }

    public void BeginDrag() // TODO: Disallow pickup of played cards
    {
        _startParent = transform.parent.gameObject;
        _startPosition = transform.position;
        _isDragging = true;
    }

    public void EndDrag()
    {
        _isDragging = false;
        if (_isDropable)
        {
            transform.SetParent(_dropZone.transform, false);
            GameModel.Instance.SetOpponentTurn();
        }
        else
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent.transform, false);
        }
    }
}
