using System;
using UnityEngine;

public class DragCards : MonoBehaviour
{

    public GameObject canvas;
    private bool _isDragging;
    private bool _isDroppable;
    private bool _isDropped;
    private bool _isNotAllowedDragging;
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
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
            transform.SetParent(canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _isDroppable = true;
        _dropZone = col.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isDroppable = false;
        _dropZone = null;
    }

    public void BeginDrag()
    {
        _isNotAllowedDragging = DragNotAllowed();
        if (_isNotAllowedDragging) return;

        _startParent = transform.parent.gameObject;
        _startPosition = transform.position;
        _isDragging = true;
    }

    private bool DragNotAllowed()
    {
        if (_isDropped) return true;
        if (!GameModel.Instance.PlayerTurn) return true;
        return false;
    }

    public void EndDrag()
    {
        if (_isNotAllowedDragging) return;
        
        _isDragging = false;
        if (_isDroppable)
        {
            DropCard();
        }
        else
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent.transform, false);
        }
    }

    private void DropCard()
    {
        transform.SetParent(_dropZone.transform, false);
        GameModel.Instance.PlayerTurn = false;
        bool wasRemoved = GameModel.Instance.RemovePlayedCard(gameObject);
        if (!wasRemoved) throw new Exception("Card not removed from internal representation");
        _isDropped = true;
    }
}