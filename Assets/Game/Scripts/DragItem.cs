using UnityEngine;
using UnityEngine.EventSystems;
public class DragItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public Transform parent;
    public TMPro.TMP_Text wordText;
    public string wordsAssign;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector2 startPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        wordText = GetComponentInChildren<TMPro.TMP_Text>();
        canvas = FindObjectOfType<Canvas>();
    }
    public void SetupUI(string value)
    {
        wordsAssign = value;
        wordText.text = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        //SlotManager.Instance.item1 = gameObject;
        rectTransform.SetParent(canvas.transform);
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / FindObjectOfType<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        DragItem dragItem = eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<DragItem>();
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.CompareTag("DropZone"))
        {
            parent = eventData.pointerCurrentRaycast.gameObject.transform;
            swapRectTransform();
        }
        else if(dragItem != null)
        {
            Transform pos;
            pos = dragItem.parent;
            dragItem.parent = parent;
            parent = pos;
            dragItem.swapRectTransform();
            swapRectTransform();

        }
        SlotManager.Instance.ChickWin();
    }
    public void swapRectTransform()
    {
        rectTransform.SetParent(parent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}