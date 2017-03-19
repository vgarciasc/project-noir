using System.Collections;
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
	float recovery = 0.25f;
	[SerializeField]
	float chunkSize = 0.25f;
	
	[HeaderAttribute("References")]
	[SerializeField]
	Slider power;
    [SerializeField]
    GameObject teleportLinePrefab;
    [SerializeField]
    GameObject teleportPlayerSurrogatePrefab;
	[SerializeField]
	Animator slowMoAnimator;
	[SerializeField]
	Animator speedMoAnimator;

	float currentPower,
		maxPower;
	int currentChunk,
		chunkQuantity;
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

	Vector3 lastPosition,
		currentPosition;

    ArenaManager ar_manager;

	void Start () {
		ar_manager = ArenaManager.getArenaManager();
		ExitSlowMo();

		power.minValue = 0;
		power.maxValue = 1;
		currentPower = maxPower = 1f;
		chunkQuantity = (int) (currentPower / chunkSize);
		currentChunk = chunkQuantity;
		
		StartCoroutine(foo());
	}

	IEnumerator foo() {
		while (true) {
			lastPosition = currentPosition;
			currentPosition = this.transform.position;
			yield return new WaitForSeconds(0.15f);		
		}
	}
	
	void Update () {
		handleInput();

		if (timeSlowed) {
			canRecover = false;
			addPower(- slowmoCost * Time.deltaTime);

			if (delayingRecovery != null) {
				StopCoroutine(delayingRecovery);
				delayingRecovery = null;
			}
		}
		// else if (timeSped) {
		// 	canRecover = false;
		// 	currentPower += slowmoCost * Time.deltaTime * 0.25f;
		// 	if (delayingRecovery != null) {
		// 		StopCoroutine(delayingRecovery);
		// 		delayingRecovery = null;
		// 	}
		// }
		else { //not using any powers
			if (delayingRecovery == null) {
				delayingRecovery = StartCoroutine(delayRecovery());
			}
		}
		
		if (canRecover) {
			addPower(recovery * Time.deltaTime);
		}

		// currentPower = Mathf.Clamp(currentPower, 0f, 1f);

		// if (currentPower <= 0f) {
		// 	cancelAllPowers();
		// }

		// if (currentPower == 1f) {
		// 	canRecover = false;
		// 	if (delayingRecovery != null) {
		// 		StopCoroutine(delayingRecovery);
		// 		delayingRecovery = null;
		// 	}
		// }

		// if (currentPower >= 1f) {
		// 	currentPower = 1f;
		// }

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
		if (Input.GetKey(KeyCode.R) && !timeSped) {
			EnterSpeedMo();
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			ExitSpeedMo();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			// Vector3 oriPos = this.transform.position;
			// Vector3 newPos = this.transform.position;
			// if (currentPosition.x < lastPosition.x) {
			// 	newPos.x = -0.5f;
			// }
			// else if (currentPosition.x > lastPosition.x) {
			// 	newPos.x = 0.5f;
			// }
			// if (currentPosition.y < lastPosition.y) {
			// 	newPos.y = 0.5f;
			// }
			// else if (currentPosition.y > lastPosition.y) {
			// 	newPos.y = -0.5f;
			// }
			
			// if (newPos.x > ar_manager.x_max || newPos.x < ar_manager.x_min ||
			// 	newPos.y > ar_manager.y_max || newPos.y < ar_manager.y_min) {
			// 		return;
			// }
			// TeleportLine line = Instantiate(teleportLinePrefab).GetComponent<TeleportLine>();
			// line.setPoints(oriPos, newPos);
			// this.transform.position = newPos;
			// if (teleportEvent != null) {
			// 	teleportEvent();
			// }
		}

        if (Input.GetMouseButtonUp(1)) {
			if (costTeleport()) {
				canRecover = false;
				if (delayingRecovery != null) {
					StopCoroutine(delayingRecovery);
					delayingRecovery = null;
				}
            	teleport();
			}
		}
	}

	void cancelAllPowers() {
		ExitSlowMo();
	}

	IEnumerator delayRecovery() {
		canRecover = false;
		yield return new WaitForSeconds(1f);
		canRecover = true;
	}

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

	//returns true if power was added/subtracted, returns false if not
	bool addPower(float amount) {
		if (amount <= 0 && currentPower >= -amount) {
			//does have enough power, subtract it
			currentPower += amount;
			updateCurrentChunk();
			return true;
		}
		else if (amount <= 0 && currentPower <= -amount) {
			//does not have enough power, set animation (visual cue), dont subtract
			power.transform.GetComponent<Animator>().SetTrigger("fizzle");
			return false;
		}
		else /*adding power*/ {
			currentPower += amount;
			currentPower = Mathf.Clamp(currentPower, 0f, currentChunk * chunkSize);

			return true;
		}
	}
	
	void updateCurrentChunk() {
		currentChunk = Mathf.FloorToInt(currentPower / chunkSize) + 1;
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
}
