using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour {
	[SerializeField]
	GameObject receiver;
	Triggerable surrogate;

	void Start() {
		surrogate = receiver.GetComponent<Triggerable>();
	}

	void OnTriggerExit2D(Collider2D target) {
		TryTriggerExit(target.gameObject, this.gameObject);
	}

	void TryTriggerExit(GameObject target, GameObject sender) {
		// Debug.Log(target.name + " collided with " + sender.name);
		surrogate.TriggerExit(target, sender);
	}

	void OnTriggerEnter2D(Collider2D target) {
		TryTriggerEnter(target.gameObject, this.gameObject);
	}

	void TryTriggerEnter(GameObject target, GameObject sender) {
		// Debug.Log(target.name + " collided with " + sender.name);
		surrogate.TriggerEnter(target, sender);
	}
}
