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

	void OnTriggerEnter2D(Collider2D target) {
		TryTriggerEnter(target.gameObject, this.gameObject);
	}

	void TryTriggerEnter(GameObject target, GameObject sender) {
		// Debug.Log(target.name + " collided with " + sender.name);
		surrogate.TriggerEnter(target, sender);
	}
}
