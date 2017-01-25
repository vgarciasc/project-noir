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
    GameObject teleportLinePrefab;
    [SerializeField]
    GameObject teleportPlayerSurrogatePrefab;

	float currentPower;
	bool timeSlowed,
		canRecover;
	Coroutine delayingRecovery;

	List<BulletDeluxe> bullets;

    ArenaManager ar_manager;

	void Start () {
		ar_manager = ArenaManager.getArenaManager();

		power.minValue = 0;
		power.maxValue = 1;
		currentPower = 1f;
		bullets = BulletPoolManager.getBulletPoolManager().getAllBullets();
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
		else { //not using any powers
			if (delayingRecovery == null) {
				delayingRecovery = StartCoroutine(delayRecovery());
			}
		}
		
		if (canRecover) {
			currentPower += recovery * Time.deltaTime;
		}

		currentPower = Mathf.Clamp(currentPower, 0f, 1f);

		if (currentPower == 0f) {
			cancelAllPowers();
		}

		if (currentPower == 1f) {
			canRecover = false;
			if (delayingRecovery != null) {
				StopCoroutine(delayingRecovery);
				delayingRecovery = null;
			}
		}

		power.value = currentPower;
	}

	void handleInput() {
		if (Input.GetKeyDown(KeyCode.R)) {
			timeSlowed = true;
			for (int i = 0; i < bullets.Count; i++) {
				bullets[i].TimeSlow(0.5f);
			}
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			timeSlowed = false;
			for (int i = 0; i < bullets.Count; i++) {
				bullets[i].ResetTimeSlow();
			}
		}
        if (Input.GetButtonDown("Fire2")) {
			if (costTeleport()) {
				canRecover = false;
				if (delayingRecovery != null) {
					StopCoroutine(delayingRecovery);
					delayingRecovery = null;
				}
            	teleport();
			}
        }
		if (Input.GetKeyDown(KeyCode.E)) {
			activateShield();
		}
	}

	#region general powers
	void cancelAllPowers() {
		timeSlowed = false;
		for (int i = 0; i < bullets.Count; i++) {
			bullets[i].ResetTimeSlow();
		}
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
			Invoke("deactivateShield", 2.0f);
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
}
