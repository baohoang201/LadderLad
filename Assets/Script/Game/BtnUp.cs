using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BtnUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    [SerializeField]
    public UnityEvent onLongClick;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Update()
    {
        if (pointerDown)
        {

            if (onLongClick != null)
            {
                onLongClick.Invoke();
                PlayerController.instance.MoveUp();
            }
            else Reset();
        }
    }

    private void Reset()
    {
        pointerDown = false;
    }
}
