using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(Selectable))]
public class HierarchyNavigationElement : MonoBehaviour, ICancelHandler
{
	private EventSystem eventSystem;
	private HierarchyNavigationGroup group;

	void Start()
	{
		group = GetComponentInParent<HierarchyNavigationGroup>();
		eventSystem = group.eventSystem;

	}

	void OnTransformParentChanged ()
	{
		Start();
	}
	public void OnCancel(BaseEventData eventData)
	{
		if(group != null)
			eventSystem.SetSelectedGameObject(group.gameObject);
	}
}
