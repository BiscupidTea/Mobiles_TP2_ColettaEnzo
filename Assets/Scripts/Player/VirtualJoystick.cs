using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform stick = null;
    [SerializeField] private Image background = null;

    public float limit = 0;

    private float horizontal;
    private float vertical;

    public float Horizontal { get => horizontal; set => horizontal = value; }
    public float Vertical { get => vertical; set => vertical = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.anchoredPosition = ConverToLocal(eventData);
    }

    private Vector2 ConverToLocal(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (transform as RectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out newPos);
        return newPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = ConverToLocal(eventData);
        if (pos.magnitude > limit)
            pos = pos.normalized * limit;

        stick.anchoredPosition = pos;

        float x = pos.x / limit;
        float y = pos.y / limit;

        horizontal = x;
        vertical = y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
        horizontal = 0;
        vertical = 0;
    }

    private void OnDisable()
    {
        horizontal = 0;
        vertical = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(background.transform.position, limit);
    }
}