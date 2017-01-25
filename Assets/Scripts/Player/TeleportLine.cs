using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLine : MonoBehaviour {
	[SerializeField]
	float lifetime = 0.5f;

	LineRenderer line;

	Vector2 start,
			end;

	float timeSinceCreation = 0f;

	Color originalColor;
	Material mat;

	void FixedUpdate() {
		timeSinceCreation += Time.deltaTime;
		line.widthMultiplier = Mathf.Lerp(1, 0, timeSinceCreation / lifetime);
		// line.SetPosition(0, Vector3.Lerp(start, end, timeSinceCreation / lifetime));
		Color aux = Color.Lerp(new Color(originalColor.r,
											originalColor.g,
											originalColor.b,
											0.5f),
								new Color(originalColor.r,
											originalColor.g,
											originalColor.b,
											0f),
								timeSinceCreation / lifetime);
		mat.SetColor("_TintColor", aux);
		line.material = mat;
	}

	public void setPoints(Vector2 start, Vector2 end) {
		line = this.GetComponent<LineRenderer>();
		line.SetPosition(0, start);
		line.SetPosition(1, end);
		this.gameObject.SetActive(true);
		StartCoroutine(kill());

		mat = Instantiate(line.material);
		originalColor = mat.GetColor("_TintColor");

		this.start = start;
		this.end = end;
	}

	IEnumerator kill() {
		yield return new WaitForSeconds(lifetime);
		Destroy(this.gameObject);
	}
}
