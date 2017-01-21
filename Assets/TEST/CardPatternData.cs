﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventInfo {
	public ShotEventData shotEvent;	

	[RangeAttribute(0f, 5f)]
	public float delayBeforeEvent;
}

[System.Serializable]
public class CardInfo {
	public ShotPatternData shot;
	public BulletData bullet;
	public EventInfo[] events;
}

[CreateAssetMenu(fileName = "Data", menuName = "Card Pattern", order = 1)]
public class CardPatternData : ScriptableObject {
	public CardInfo[] array;
}
