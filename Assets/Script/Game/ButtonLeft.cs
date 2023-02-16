using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class ButtonLeft : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
         PlayerController.instance.MoveLeft();
    }
}
