using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFragment : MonoBehaviour, Triggerable {

	MemoryFragmentManager mem_manager;

	void Start() {
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
	}

	public void TriggerEnter(GameObject sender, GameObject receiver) {
		if (sender.gameObject.tag == "PlayerCollider") {
			InterrogationManager.getInterrogationManager().captureMemoryFragment();
		}
	}

	public void TriggerExit(GameObject sender, GameObject receiver) {
	}

	void AnimGetMemory() {
		mem_manager.fullMemory();
	}
}
