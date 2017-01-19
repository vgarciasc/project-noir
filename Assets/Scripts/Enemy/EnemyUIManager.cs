using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour {
	[SerializeField]
	Slider healthSlider;
	[SerializeField]
	BossTitleCardManager btcmanager;
	[SerializeField]
	BossSpecialCardManager bscmanager;
	[SerializeField]
	GameObject gameOver;

	Enemy enemy;

	void Start () {
		enemy = this.GetComponent<Enemy>();
		enemy.set_health_event += set_health;
	}

	void Update() {
		update_health();
	}
	
	#region Health
	void start_boss_hud() {
		healthSlider.GetComponentInParent<Animator>().SetTrigger("intro");
		btcmanager.intro();
	}

	void update_health() {
		healthSlider.value = enemy.current_health;
	}

	void set_health(int health) {
		healthSlider.minValue = 0;
		healthSlider.maxValue = health;
	}
	#endregion
	
	#region Form
	public void newForm(string form_text) {
		bscmanager.setText(form_text);
		bscmanager.show();
	}

	public void end_battle() {
		gameOver.SetActive(true);
	}
	#endregion
}
