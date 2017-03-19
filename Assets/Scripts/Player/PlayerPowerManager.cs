﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerManager : MonoBehaviour {
	[HeaderAttribute("Power Values")]
	[SerializeField]
	float teleportCost = 0.5f;
	[SerializeField]
	float slowmoCost = 0.5f;
	[SerializeField]
	float shieldCost = 1f;
	[SerializeField]
	float recovery = 0.25f;
	
	[HeaderAttribute("References")]
	[SerializeField]
	Slider power;
	[SerializeField]
	GameObject shield;
	[SerializeField]
	GameObject shieldExplorer;
    [SerializeField]
    GameObject teleportLinePrefab;
    [SerializeField]
    GameObject teleportPlayerSurrogatePrefab;
	[SerializeField]
	Animator slowMoAnimator;
	[SerializeField]
	Animator speedMoAnimator;

	float currentPower;
	bool timeSlowed,
		timeSped,
		canRecover;
	Coroutine delayingRecovery;

	public delegate void CallPowerDelegate();
	public event CallPowerDelegate teleportEvent;
	
	//TODO: REFACTOR
	public delegate void CallDeathDelegate();
	public event CallDeathDelegate deathEvent;

	// List<BulletDeluxe> bullets;

    ArenaManager ar_manager;

	void Start () {
		ar_manager = ArenaManager.getArenaManager();
		ExitSlowMo();

		power.minValue = 0;
		power.maxValue = 1;
		currentPower = 1f;
		// bullets = BulletPoolManager.getBulletPoolManager().getAllBullets();
	}
	
	void Update () {
		handleInput();

		if (timeSlowed) {
			canRecover = false;
			currentPower -= slowmoCost * Time.deltaTime;
			if (delayingRecovery != null) {
				StopCoroutine(delayingRecovery);
				delayingRecovery = null;
			}
		}
		else if (timeSped) {
			canRecover = false;
			currentPower += slowmoCost * Time.deltaTime * 0.25f;
			if (delayingRecovery != null) {
				StopCoroutine(delayingRecovery);
				delayingRecovery = null;
			}
		}
		else { //not using any powers
			if (delayingRecovery == null) {
				delayingRecovery = StartCoroutine(delayRecovery());
			}
		}
		
		if (canRecover) {
			currentPower += recovery * Time.deltaTime;
		}

		currentPower = Mathf.Clamp(currentPower, 0f, 1f);

		if (currentPower <= 0f) {
			cancelAllPowers();
		}

		if (currentPower == 1f) {
			canRecover = false;
			if (delayingRecovery != null) {
				StopCoroutine(delayingRecovery);
				delayingRecovery = null;
			}
		}

		if (currentPower >= 1f) {
			currentPower = 1f;
		}

		power.value = currentPower;
	}

	float timeSpentPressingM2 = 0f;

	void handleInput() {
		if (Input.GetButton("Fire1") && !timeSlowed) {
			EnterSlowMo();
		}
		if (Input.GetButtonUp("Fire1")) {
			ExitSlowMo();
		}
		if (Input.GetButton("Fire2") && !timeSped) {
			EnterSpeedMo();
		}
		if (Input.GetButtonUp("Fire2")) {
			ExitSpeedMo();
		}
        // if (Input.GetMouseButtonUp(1)) {
		// 	if (timeSpentPressingM2 > 3f) {
		// 		activateShield();
		// 		canRecover = false;
		// 		if (delayingRecovery != null) {
		// 			StopCoroutine(delayingRecovery);
		// 			delayingRecovery = null;
		// 		}
        //     	teleport();
		// 	}
			
		// 	if (costTeleport()) {
		// 		canRecover = false;
		// 		if (delayingRecovery != null) {
		// 			StopCoroutine(delayingRecovery);
		// 			delayingRecovery = null;
		// 		}
        //     	teleport();
		// 	}

		// 	timeSpentPressingM2 = 0f;
        // }
		// if (Input.GetMouseButton(1)) {
		// 	timeSpentPressingM2 += Time.deltaTime;
		// }

		// shieldExplorer.SetActive(timeSpentPressingM2 > 3f);
	}

	#region general powers
	void cancelAllPowers() {
		ExitSlowMo();
	}

	IEnumerator delayRecovery() {
		yield return new WaitForSeconds(1f);
		canRecover = true;
	}

	#region teleport
	void teleport() {
		Vector2 originalPos = this.transform.position;
		Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (newPos.x > ar_manager.x_max || newPos.x < ar_manager.x_min ||
			newPos.y > ar_manager.y_max || newPos.y < ar_manager.y_min) {
				return;
		}

		this.transform.position = newPos;
		GameObject player_surrogate = Instantiate(teleportPlayerSurrogatePrefab,
												originalPos,
												Quaternion.identity);
		player_surrogate.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
		player_surrogate.transform.localScale = this.transform.localScale;
		player_surrogate.GetComponent<Animator>().SetTrigger("despawn");
		this.GetComponent<Animator>().SetTrigger("spawn");

		TeleportLine line = Instantiate(teleportLinePrefab).GetComponent<TeleportLine>();
		line.setPoints(originalPos, newPos);
		this.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //now not falling

		if (teleportEvent != null) {
			teleportEvent();
		}
	}

	bool costTeleport() {
		if (currentPower >= teleportCost) {
			currentPower -= teleportCost;
			return true;
		}

		power.transform.GetComponent<Animator>().SetTrigger("fizzle");
		return false;
	}
	#endregion

	#region shield
	bool shieldActive = false;

	void activateShield() {
		if (costShield()) {
			GetComponent<Animator>().SetBool("shield_active", true);
			shield.SetActive(true);
			Invoke("deactivateShield", 5.0f);
		}
	}
	
	void deactivateShield() {
		GetComponent<Animator>().SetBool("shield_active", false);
	}

	void AnimDeactivateShield() {
		shield.SetActive(false);
	}

	bool costShield() {
		if (currentPower >= shieldCost) {
			currentPower = 0f;
			return true;
		}

		power.transform.GetComponent<Animator>().SetTrigger("fizzle");
		return false;
	}
	#endregion
	#endregion

	void EnterSlowMo() {
		slowMoAnimator.SetBool("slowmo", true);
		timeSlowed = true;
		Time.timeScale = 0.5f;
      	// Time.fixedDeltaTime = 0.02F * Time.timeScale;

		// foreach (Animator anim in GameObject.FindObjectsOfType(typeof(Animator))) {
		// 	anim.speed = 0.5f;
		// }

		// for (int i = 0; i < bullets.Count; i++) {
		// 	bullets[i].TimeSlow(0.5f);
		// }
	}

	void ExitSlowMo() {
		slowMoAnimator.SetBool("slowmo", false);
		timeSlowed = false;
		Time.timeScale = 1f;
      	// Time.fixedDeltaTime = 0.02F * Time.timeScale;

		// foreach (Animator anim in GameObject.FindObjectsOfType(typeof(Animator))) {
		// 	anim.speed = 1f;
		// }

		// for (int i = 0; i < bullets.Count; i++) {
		// 	bullets[i].ResetTimeSlow();
		// }
	}

	void EnterSpeedMo() {
		speedMoAnimator.SetBool("slowmo", true);
		timeSped = true;
		Time.timeScale = 2f;
	}

	void ExitSpeedMo() {
		speedMoAnimator.SetBool("slowmo", false);
		timeSped = false;
		Time.timeScale = 1f;
	}

	bool detractPower(float amount) {
		if (currentPower >= amount) {
			currentPower -= amount;
			return true;
		}

		power.transform.GetComponent<Animator>().SetTrigger("fizzle");
		return false;
	}

	public void minusHealth(float healthLost) {
		if (currentPower < healthLost / 10f && deathEvent != null) {
			deathEvent();
		}

		detractPower(healthLost / 10f);

		canRecover = false;
		if (delayingRecovery != null) {
			StopCoroutine(delayingRecovery);
			delayingRecovery = null;
		}
	}
}
