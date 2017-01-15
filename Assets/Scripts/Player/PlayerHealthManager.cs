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
	
	public int currentHealth;
	public int maxHealth;

	PlayerGeneral player;

	void Start () {
		player = this.GetComponent<PlayerGeneral>();

		player.take_hit_event += take_hit;
		player.instakill_event += instakill;
		startHealth();
	}

	void startHealth() {
		currentHealth = maxHealth;
		for (int i = 0; i < maxHealth; i++) {
			GameObject obj = Instantiate(healthIconPrefab, healthIconContainer, false);
		}
	}

	void take_hit() {
		if (currentHealth == 0) {
			SpecialCamera.getSpecialCamera().screenShake_(0.25f);
			SpecialCamera.getSpecialCamera().screenShake_(0.25f);
			SpecialCamera.getSpecialCamera().screenShake_(0.25f);
			restartButton.gameObject.SetActive(true);
			return;
		}

		SpecialCamera.getSpecialCamera().screenShake_(0.25f);
		currentHealth--;
		Transform trf = healthIconContainer.GetChild(currentHealth);
		trf.GetComponent<Image>().color = HushPuppy.getColorWithOpacity(trf.GetComponent<Image>().color, 0.3f);
	}

	void instakill() {
		currentHealth = 0;
		take_hit();
	}

}
