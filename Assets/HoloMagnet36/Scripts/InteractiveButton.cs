using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveButton : MonoBehaviour, IInputClickHandler
{
	[SerializeField]private UnityEvent onClickEvent = new UnityEvent();

	public void OnInputClicked(InputClickedEventData eventData)
	{
        eventData.Use();

		onClickEvent.Invoke();

		return;
	}
}
