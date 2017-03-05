using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableEnemyShot : MonoBehaviour {
	
	MemoryFragmentManager mem_manager;
	InterrogationManager interrogation_manager;
	
	Animator animator;

	void Start () {
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		interrogation_manager = InterrogationManager.getInterrogationManager();
		animator = this.GetComponent<Animator>();
		
		mem_manager.pauseEvent += pauseAnimation;
		mem_manager.unpauseEvent += unpauseAnimation;
		foreach (EnemyAnimatorManager e in GameObject.FindObjectsOfType<EnemyAnimatorManager>()) {
			// e.startWrongObjection += pauseAnimation;
			// e.endWrongObjection += unpauseAnimation;
			e.startWrongObjection += smoothStopAnimation;
		}
	}
	
	void pauseAnimation() {
		animator.speed = 0;
		// animator.SetTrigger("pause");
		// AnimStopCurrentCard();
	}

	void unpauseAnimation() {
		animator.speed = 1;
		// animator.SetTrigger("restart");
	}

	void stopAnimation() {
		this.gameObject.SetActive(false);
	}

	void smoothStopAnimation() {
		animator.SetTrigger("destroy");
	}
}
