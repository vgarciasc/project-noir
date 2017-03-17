using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {
	[SerializeField]
	GameObject healthIconPrefab;
	[SerializeField]
	Transform healthIconContainer;
	[SerializeField]
	Transform restartButton;
	[SerializeField]
	GameObject mainCollider;
	
	public int currentHealth;
	public int maxHealth;

	bool invincible = false;

	PlayerGeneral player;
	PlayerPowerManager playerPower;
	MemoryFragmentManager mem_manager;
	InterrogationManager interrogation;

	void Start () {
		player = this.GetComponent<PlayerGeneral>();
		playerPower = this.GetComponent<PlayerPowerManager>();
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		interrogation = InterrogationManager.getInterrogationManager();

		player.take_hit_event += take_hit;
		player.instakill_event += instakill;
		mem_manager.pauseEvent += AnimDeactivateCollider;
		mem_manager.unpauseEvent += AnimActivateCollider;
		interrogation.startPressEvent += shockwave;

		startHealth();

		//TODO: TODELETE
		playerPower.deathEvent += die;
	}

	//TODELETE
	void shockwave() {
		this.GetComponent<Animator>().SetTrigger("shockwave_activate");
	}

	void startHealth() {
		currentHealth = maxHealth;
		// for (int i = 0; i < maxHealth; i++) {
		// 	GameObject obj = Instantiate(healthIconPrefab, healthIconContainer, false);
		// }
	}

	void take_hit() {
		if (invincible) {
			return;
		}

		Transform trf;
		playerPower.minusHealth(1);

		// currentHealth--;
		if (currentHealth < 0) {
			return;
		}

		if (currentHealth == 0) {
			die();
			return;
		}

		invincible = true;
		this.GetComponent<Animator>().SetTrigger("take_hit");
		SpecialCamera.getSpecialCamera().screenShake_(0.25f);
		// trf = healthIconContainer.GetChild(currentHealth);
		// trf.GetComponent<Image>().color = HushPuppy.getColorWithOpacity(trf.GetComponent<Image>().color, 0.3f);
	}

	void instakill() {
		currentHealth = 1;
		take_hit();
	}

	void AnimDeactivateCollider() {
		invincible = true;
	}

	void AnimActivateCollider() {
		invincible = false;
	}

	public void BulletHit(int damage) {
		for (int i = 0; i < damage; i++) {
			take_hit();
		}
	}

	void die() {
		Transform trf;

		SpecialCamera.getSpecialCamera().screenShake_(0.25f);
		SpecialCamera.getSpecialCamera().screenShake_(0.25f);
		SpecialCamera.getSpecialCamera().screenShake_(0.25f);
		
		restartButton.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
		// trf = healthIconContainer.GetChild(currentHealth);
		// trf.GetComponent<Image>().color = HushPuppy.getColorWithOpacity(trf.GetComponent<Image>().color, 0.3f);
	}
}