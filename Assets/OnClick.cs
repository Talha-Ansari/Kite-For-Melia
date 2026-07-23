using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] UnityEvent moveEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        moveEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Player3D.instance.Stop();
        Player.Instance.Stop();
    }


}
