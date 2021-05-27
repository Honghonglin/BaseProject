using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class EventTriggerListener : EventTrigger
{
	public delegate void VoidDelegate(GameObject go);
	public VoidDelegate onClick;
	public VoidDelegate onDown;
	public VoidDelegate onEnter;
	public VoidDelegate onExit;
	public VoidDelegate onUp;
	public VoidDelegate onSelect;
	public VoidDelegate onUpdateSelect;
	public VoidDelegate onPointer;
	private bool isDown=false;
	private void Update()
	{
		if (isDown)
		{
			print(gameObject.name);
			m_OnLongpress();
		}
	}
	static public EventTriggerListener Get(GameObject go)
	{
		EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
		if (listener == null) listener = go.AddComponent<EventTriggerListener>();
		return listener;
	}
	private void m_OnLongpress()
    {
		if (onPointer != null) onPointer(gameObject);
	}
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (onClick != null) onClick(gameObject);
	}
	public override void OnPointerDown(PointerEventData eventData)
	{
		isDown = true;
		if (onDown != null) onDown(gameObject);
	}
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (onEnter != null) onEnter(gameObject);
	}
	public override void OnPointerExit(PointerEventData eventData)
	{
		if (onExit != null) onExit(gameObject);
	}
	public override void OnPointerUp(PointerEventData eventData)
	{
		isDown = false;
		if (onUp != null) onUp(gameObject);
	}
	public override void OnSelect(BaseEventData eventData)
	{
		if (onSelect != null) onSelect(gameObject);
	}
	public override void OnUpdateSelected(BaseEventData eventData)
	{
		if (onUpdateSelect != null) onUpdateSelect(gameObject);
	}
}