using UnityEngine;

public class CardZoom : MonoBehaviour
{

    public GameObject canvas;
    private GameObject _zoomCard;

    private void Awake()
    {
        canvas = GameObject.Find("MainCanvas");
    }

    public void OnClickEnter()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _zoomCard = Instantiate(gameObject, canvas.transform, false);
        _zoomCard.transform.position = new Vector3(worldPoint.x, worldPoint.y + 2f, 0);
        _zoomCard.transform.localScale = new Vector3(2f, 2f, 1f);
        _zoomCard.layer = LayerMask.NameToLayer("Zoom");
        
        RectTransform cardRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform rectTransform = _zoomCard.GetComponent<RectTransform>();
        Rect rect = cardRectTransform.rect;
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }

    public void OnClickExit()
    {
        Destroy(_zoomCard);
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
