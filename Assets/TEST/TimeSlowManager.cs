using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowManager : MonoBehaviour {

	List<BulletDeluxe> bullets;

	public bool slowed = false;

	void Start() {
		bullets = BulletPoolManager.getBulletPoolManager().getAllBullets();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			slowed = true;
			for (int i = 0; i < bullets.Count; i++) {
				bullets[i].TimeSlow(0.5f);
			}
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			slowed = false;
			for (int i = 0; i < bullets.Count; i++) {
				bullets[i].ResetTimeSlow();
			}
		}
	}
}
