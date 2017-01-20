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
				checkShotPatternData(shot_data);

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
					BulletDeluxe bullet = create_bullet(shot_data,
														bullet_data,
														emitter.position,
														angle);
					set_bullet_visuals(shot_data, bullet, i, h, j);
					set_bullet_sinusoidal(shot_data, bullet, i, h, j);
                }

                for (int k = 0; j % shot_data.delayAppliedEachNBullets == 0 &&
								k < shot_data.delayBetweenBullets; k++)
                    yield return new WaitForFixedUpdate();
            }

            for (int k = 0; k < shot_data.delayBetweenWaves; k++)
                yield return new WaitForFixedUpdate();
        }
    }

	BulletDeluxe create_bullet(ShotPatternData shot_data, BulletData bullet_data, Vector3 position, float angle_grad) {
		BulletDeluxe bullet = pool.getNewBullet().GetComponent<BulletDeluxe>();
		bullet.activate();

		bullet.setData(bullet_data, player);

		if (!bullet_data.rotateDirectionShot) {
			bullet.GetComponentInChildren<ComponentFixRotation>().enabled = true;
		}

		bullet.setRotation(angle_grad);
		bullet.setPosition(position + bullet.transform.up * shot_data.bulletInitialPositionOffset);
		bullet.setSpeed(shot_data.bulletSpeed);

		return bullet;
	}

	void set_bullet_visuals(ShotPatternData shot_data, BulletDeluxe bullet, int wave_ID, int thread_ID, int bullet_ID) {
		Color color;
		Sprite sprite;

		switch (shot_data.coloringStyle) {
			default:
			case ShotPatternVisualStyle.BULLET_INTERLACED:
				color = shot_data.colors[bullet_ID % shot_data.colors.Length];
				break;
			case ShotPatternVisualStyle.THREAD_INTERLACED:
				color = shot_data.colors[thread_ID % shot_data.colors.Length];
				break;
			case ShotPatternVisualStyle.WAVE_INTERLACED:
				color = shot_data.colors[wave_ID % shot_data.colors.Length];
				break;
			case ShotPatternVisualStyle.RANDOM:
				color = shot_data.colors[(int) (Time.time) % shot_data.colors.Length];
				break;
		}

		switch (shot_data.spritingStyle) {	
			default:
			case ShotPatternVisualStyle.BULLET_INTERLACED:
				sprite = shot_data.sprites[bullet_ID % shot_data.sprites.Length];
				break;
			case ShotPatternVisualStyle.THREAD_INTERLACED:
				sprite = shot_data.sprites[thread_ID % shot_data.sprites.Length];
				break;
			case ShotPatternVisualStyle.WAVE_INTERLACED:
				sprite = shot_data.sprites[wave_ID % shot_data.sprites.Length];
				break;
			case ShotPatternVisualStyle.RANDOM:
				sprite = shot_data.sprites[(int) (Time.time) % shot_data.sprites.Length];
				break;
		}

		bullet.setColor(color);
		bullet.setSprite(sprite);
	}

	void set_bullet_sinusoidal(ShotPatternData shot_data, BulletDeluxe bullet, int wave_ID, int thread_ID, int bullet_ID) {
		switch (shot_data.sinusoidalMotion) {
			case ShotPatternSinusoidalStyle.ALL_COSINE:
				bullet.startCosineMovement();
				break;
			case ShotPatternSinusoidalStyle.ALL_SINE:
				bullet.startSineMovement();
				break;
			case ShotPatternSinusoidalStyle.THREAD_INTERLACED:
				if (thread_ID % 2 == 0) {
					bullet.startCosineMovement();
				}
				else {
					bullet.startSineMovement();
				}
				break;
			case ShotPatternSinusoidalStyle.WAVE_INTERLACED:
				if (wave_ID % 2 == 0) {
					bullet.startCosineMovement();
				}
				else {
					bullet.startSineMovement();
				}
				break;
			default: case ShotPatternSinusoidalStyle.NONE:
				break;
		}
	}

	void checkShotPatternData(ShotPatternData data) {
		if (data.colors.Length == 0) {
			Debug.Log("No bullet color chosen. ");
			Debug.Break();
		}
		if (data.colors.Length == 0) {
			Debug.Log("No bullet sprite chosen. ");
			Debug.Break();
		}
	}
}
