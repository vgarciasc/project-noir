using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCardEmitter : CardEmitter {

	public int parts;
	
	[RangeAttribute(0, 60)]
	public int delayBetweenShots;

	public Transform lineStart,
		lineEnd;
	
	List<GameObject> emitterSurrogates;
	[SerializeField]
	GameObject emitterSurrogatePrefab;

	public delegate void EmitDelegate();
	public event EmitDelegate emitted_shot;

	void Start() {
		pool = BulletPoolManager.getBulletPoolManager();
		
		emitterSurrogates = new List<GameObject>();
		
		StartCoroutine(emit());
	}

	IEnumerator emit() {
		for (int i = 0; i < parts; i++) {
			Vector2 point = Vector2.Lerp(lineStart.position, lineEnd.position, ((float) i) / (parts - 1));
			GameObject aux = Instantiate(emitterSurrogatePrefab,
										new Vector3(point.x, point.y, 0f),
										Quaternion.identity);
			aux.transform.SetParent(this.transform);
			emitterSurrogates.Add(aux);

			PlayCard(testCard, aux.transform);

			if (emitted_shot != null) {
				emitted_shot();
			}

			for (int k = 0; k < delayBetweenShots; k++)
				yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(2.0f);
		for (int i = 0; i < emitterSurrogates.Count; i++) {
			Destroy(emitterSurrogates[i]);
		}
	}
}
