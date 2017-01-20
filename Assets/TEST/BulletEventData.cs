using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSelection { ALL_BULLETS, THIS_SHOT_BULLETS }
public enum BulletAction { PLAYER_DIRECTION }

public class BulletEventData : ScriptableObject {
	
	public BulletSelection affectsWhichBullets;
	
	public BulletAction whatAction;
	
}
