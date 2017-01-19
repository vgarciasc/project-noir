using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEmitter : MonoBehaviour {
	public CardPatternData card;
	public bool startShooting = true;
	public Transform emitter;

	BulletPoolManager pool;
	Transform player;

	void Start () {
		pool = BulletPoolManager.getBulletPoolManager();
		player = GameObject.FindGameObjectWithTag("Player").transform;

		if (startShooting) {
			StartCoroutine(playCard(card));
		}
	}

	IEnumerator playCard(CardPatternData card) {
		for (int i = 0; i < card.array.Length; i++) {
			ShotPatternData shot_data = Instantiate(card.array[i].shot);

			for (int j = 0; j < card.array[i].shot.loopQuantity; j++) {
				StartCoroutine(shoot(shot_data, card.array[i].bullet, emitter));

				for (int k = 0; k < card.array[i].shot.delayBetweenLoops; k++)
                	yield return new WaitForFixedUpdate();
			}
		}
	}
	
	// IEnumerator shoot(ShotPatternData shot_data, BulletData bullet_data) {
	// 	float angle_increment = (shot_data.angleOffset + shot_data.threadArcAngle * Mathf.PI) / (shot_data.bulletNumber * 180f);

	// 	for (int i = 0; i < shot_data.loopTimes; i++) {
	// 		for (int j = 0; j < shot_data.bulletNumber; j++) {
	// 			for (int l = 0; l < shot_data.threadQuantity; l++) {
	// 				float bullet_angle = j * angle_increment;
	// 				bullet_angle += (shot_data.threadsTotalArcAngle / shot_data.threadQuantity) * l;

	// 				Vector2 direction = new Vector2(Mathf.Cos(bullet_angle), Mathf.Sin(bullet_angle));

	// 				create_bullet(bullet_data,
	// 							emitter.position,
	// 							bullet_angle,
	// 							direction);

	// 				for (int k = 0; j % shot_data.applyDelayEachNBullets == 0 && 
	// 								k < shot_data.delayBetweenBullets; k++)
	// 					yield return new WaitForFixedUpdate();
	// 			}
	// 		}

	// 		for (int k = 0; k < shot_data.delayBetweenLoops; k++)
	// 			yield return new WaitForFixedUpdate();
	// 	}
	// }

	 IEnumerator shoot(ShotPatternData shot_data, BulletData bullet_data, Transform emitter) {
        // float alternatingAngle = 360 / (shot_data.threadQuantity * 2);
		float bullet_angle = shot_data.angleOffset;
		bullet_angle += emitter.rotation.z * 180;
		if (shot_data.playerDirection) {
			bullet_angle = Vector2.Angle(emitter.position - player.position, this.transform.up) + 220;
		}
		float original_bullet_angle = bullet_angle;

        int current_bullet_ID = 0;

        for (int i = 0; i < shot_data.waveQuantity; i++) {
			if (!shot_data.waveWrap) {
				bullet_angle = original_bullet_angle;
			}

			bullet_angle += shot_data.threadArcAngleIncrementBetweenWaves;

            for (int j = 0; j < shot_data.bulletQuantity; j++) {
				float bullet_angle_increment = shot_data.threadArc / shot_data.bulletQuantity;
				if ((shot_data.waveWrap && i % 2 == 0)) {
					bullet_angle_increment *= -1;
				}
				if (shot_data.clockwise) {
					bullet_angle_increment *= -1;
				}

				bullet_angle += bullet_angle_increment;

                for (int h = 0; h < shot_data.threadQuantity; h++) {
                    current_bullet_ID++;

					float angle = bullet_angle + ((shot_data.angleBetweenThreads / shot_data.threadQuantity) * h);
					create_bullet(shot_data,
								bullet_data,
								emitter.position,
								angle);
                }

                for (int k = 0; j % shot_data.delayAppliedEachNBullets == 0 &&
								k < shot_data.delayBetweenBullets; k++)
                    yield return new WaitForFixedUpdate();
            }

            for (int k = 0; k < shot_data.delayBetweenWaves; k++)
                yield return new WaitForFixedUpdate();
        }
    }

	void create_bullet(ShotPatternData shot_data, BulletData bullet_data, Vector3 position, float angle_grad) {
		BulletDeluxe bullet = pool.getNewBullet().GetComponent<BulletDeluxe>();
		bullet.activate();

		bullet.setData(bullet_data, player);

		if (!bullet_data.rotateDirectionShot) {
			bullet.GetComponentInChildren<ComponentFixRotation>().enabled = true;
		}

		bullet.setRotation(angle_grad);
		bullet.setPosition(position + bullet.transform.up * shot_data.bulletInitialPositionOffset);
		bullet.setSpeed(shot_data.bulletSpeed);
		
		if (shot_data.sineMovement) bullet.toggleSineMovement(true);
		// if (shot_data.playerDirection) bullet.transform.up = bullet.transform.position - player.position;

		// bullet.addAccelerationEmitterDirection(5f);
		// bullet.addAcceleration(bullet.transform.up * -1, data.initialSpeed);
	}
}
