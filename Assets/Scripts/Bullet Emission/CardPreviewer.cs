using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CardEmitter))]
public class CardPreviewer : MonoBehaviour {

	[SerializeField]
	ShotPatternData shot;	

	Transform emitter;
	Transform player;

	List<float> angles;

	void Update () {
		if (Application.isEditor && !Application.isPlaying) {
			emitter = this.GetComponent<CardEmitter>().emitter;
			player = GameObject.FindGameObjectWithTag("Player").transform;
			
			if (shot != null) {
				angles = getAngles(shot);

				for (int i = 0; i < angles.Count; i++) {
					Debug.DrawRay(this.transform.position,
								HushPuppy.rotateVector(Vector2.up * 3, angles[i]),
								Color.red,
								1f);
				}
			}
		}
	}

	List<float> getAngles(ShotPatternData shot_data) {
		List<float> angles = new List<float>();

		float bullet_angle = shot_data.angleOffset;
		bullet_angle += emitter.rotation.z * 180;
		if (shot_data.playerDirection) {
			bullet_angle = Vector2.Angle(emitter.position - player.position, this.transform.up) + 220;
		}
		float original_bullet_angle = bullet_angle;

        for (int i = 0; i < shot_data.waveQuantity; i++) {
			if (!shot_data.waveWrap) {
				bullet_angle = original_bullet_angle;
			}

			for (int h = 0; h < shot_data.threadQuantity; h++) {
				float angle = bullet_angle + ((shot_data.angleBetweenThreads / shot_data.threadQuantity) * h);
				angles.Add(angle);
			}
        }

		return angles;
	}
}
