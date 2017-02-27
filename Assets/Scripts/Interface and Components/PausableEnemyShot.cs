using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableEnemyShot : MonoBehaviour {
	
	MemoryFragmentManager mem_manager;
	Animator animator;

	void Start () {
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		animator = this.GetComponent<Animator>();
		
		mem_manager.pauseEvent += pauseAnimation;
		mem_manager.unpauseEvent += unpauseAnimation;
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
}
