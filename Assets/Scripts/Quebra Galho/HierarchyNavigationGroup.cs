using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[ExecuteInEditMode]
public class HierarchyNavigationGroup : Selectable, ISubmitHandler
{
	public enum Mode{Horizontal, Vertical}
	public EventSystem eventSystem;

	public Mode mode;	
	private List<Selectable> hierarchyNavigationElements = new List<Selectable>();

	private void UpdateHierarchyNavigationElements()
	{
		foreach (Selectable sel in hierarchyNavigationElements)
			sel.navigation = Navigation.defaultNavigation;
		hierarchyNavigationElements.Clear();

		for (int i = 0; i < transform.childCount; i++)
		{
			Selectable sel;
			if ((sel = transform.GetChild(i).GetComponent<Selectable>()) != null)
			{
				hierarchyNavigationElements.Add(sel);
			}
		}
	}

	private void OrganizeHierarchyNavigation()
	{		
		for (int i = 0; i < hierarchyNavigationElements.Count; i++)
		{			
			Navigation nav = new Navigation();
			nav.mode = Navigation.Mode.Explicit;

			if (i + 1 < hierarchyNavigationElements.Count)
			{
				if(mode == Mode.Horizontal)
					nav.selectOnDown = hierarchyNavigationElements[i + 1]; 
				else
					nav.selectOnRight = hierarchyNavigationElements[i + 1];
			}

			if (i > 0)
			{
				if (mode == Mode.Horizontal)
					nav.selectOnUp = hierarchyNavigationElements[i - 1];
				else
					nav.selectOnLeft = hierarchyNavigationElements[i - 1];
			}

			hierarchyNavigationElements[i].navigation = nav;
		}

		
	}

	protected override void Start()
	{
		base.Start();
		if(eventSystem == null)
			eventSystem = GetComponentInParent<EventSystem>();
		if (eventSystem == null)
			eventSystem = EventSystem.current;
		UpdateHierarchyNavigationElements();
		OrganizeHierarchyNavigation();
	}

	public void OnTransformChildrenChanged()
	{
		UpdateHierarchyNavigationElements();
		OrganizeHierarchyNavigation();
	}	

	public void OnSubmit(BaseEventData eventData)
	{
		if(hierarchyNavigationElements.Count > 0)
			eventSystem.SetSelectedGameObject(hierarchyNavigationElements[0].gameObject);
	}
}
