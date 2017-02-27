using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCutscene : MonoBehaviour {
	
	MemoryFragmentManager mem_manager;

	void Start() {
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
	}
	
	void AnimUnpause() {
		mem_manager.endFullMemory();
	}
}
