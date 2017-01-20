using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEventManager : MonoBehaviour {
	public static BulletEventManager getBulletEventManager() {
		return (BulletEventManager) HushPuppy.safeFindComponent("GameController", "BulletEventManager");
	}
}
