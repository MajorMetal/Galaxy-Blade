
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public enum PointerEventTriggerType
{
	PointerEnter,
	PointerExit,
	PointerDown,
	PointerUp,
	PointerClick
}

[AddComponentMenu("Event/Pointer Event Trigger")]
public class PointerEventTrigger :
	MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler,
	IPointerDownHandler,
	IPointerUpHandler,
	IPointerClickHandler
{
	[Serializable]
	public class PointerTriggerEvent : UnityEvent<PointerEventData>
	{
	}
	
	[Serializable]
	public class Entry
	{
		public PointerEventTriggerType eventID = PointerEventTriggerType.PointerClick;
		public PointerTriggerEvent callback = new PointerTriggerEvent ();
	}
	
	public List<Entry> delegates;
	
	protected PointerEventTrigger()
	{
	}
	
	private void Execute<T>(PointerEventTriggerType id, T eventData) where T : PointerEventData
	{
		if (delegates != null)
		{
			for (int i = 0, imax = delegates.Count; i < imax; ++i)
			{
				Entry ent = delegates[i];
				if (ent.eventID == id && ent.callback != null)
					ent.callback.Invoke (eventData);
			}
		}
	}
	
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		Execute (PointerEventTriggerType.PointerEnter, eventData);
	}
	
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		Execute (PointerEventTriggerType.PointerExit, eventData);
	}
	
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		Execute (PointerEventTriggerType.PointerDown, eventData);
	}
	
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		Execute (PointerEventTriggerType.PointerUp, eventData);
	}
	
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		Execute (PointerEventTriggerType.PointerClick, eventData);
	}
}
