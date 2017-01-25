using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyBulletsOnContact : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Bullet") {
			target.GetComponent<BulletDeluxe>().destroy();
		}
	}
}
