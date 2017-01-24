using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLine : MonoBehaviour {
	[SerializeField]
	float lifetime = 0.5f;

	LineRenderer line;

	void Start() {
	}

	public void setPoints(Vector2 start, Vector2 end) {
		line = this.GetComponent<LineRenderer>();
		line.SetPosition(0, start);
		line.SetPosition(1, end);
		this.gameObject.SetActive(true);
		StartCoroutine(kill());
	}

	IEnumerator kill() {
		yield return new WaitForSeconds(lifetime);
		Destroy(this.gameObject);
	}
}
