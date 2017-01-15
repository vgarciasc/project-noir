using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {
	[SerializeField]
	GameObject[] borders;

	public float x_min, x_max, y_min, y_max;

	public static ArenaManager getArenaManager() {
		return (ArenaManager) HushPuppy.safeFindComponent("GameController", "ArenaManager");
	}

	void Start () {
		startBorders();
	}

	void startBorders() {
		float x_min = 200, 
			x_max = 0,
			y_min = 200,
			y_max = 0;

		foreach (GameObject obj in borders) {
			if (obj.transform.position.x < x_min) {
				x_min = obj.transform.position.x;
			}
			if (obj.transform.position.x > x_max) {
				x_max = obj.transform.position.x;
			}
			if (obj.transform.position.y < y_min) {
				y_min = obj.transform.position.y;
			}
			if (obj.transform.position.y > y_max) {
				y_max = obj.transform.position.y;
			}
		}

		this.x_min = x_min;
		this.x_max = x_max;
		this.y_min = y_min;
		this.y_max = y_max;
	}
}
