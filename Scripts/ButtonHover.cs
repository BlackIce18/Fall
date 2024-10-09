using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Serializable]
    public class ButtonHoverEvent : UnityEvent { }
    [Serializable]
    public class ButtonPointerExitEvent : UnityEvent { }

    [SerializeField]
    private ButtonHoverEvent _hoverEvent = new ButtonHoverEvent();
    [SerializeField]
    private ButtonPointerExitEvent _PointerExitEvent = new ButtonPointerExitEvent();
    public ButtonHoverEvent OnButtonHover { get { return _hoverEvent; } set { Debug.Log(value); _hoverEvent = value; } }
    public ButtonPointerExitEvent OnPointerExitEvent { get { return _PointerExitEvent; } set { _PointerExitEvent = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        //OnButtonHover.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        OnPointerExitEvent.Invoke();
    }
}
