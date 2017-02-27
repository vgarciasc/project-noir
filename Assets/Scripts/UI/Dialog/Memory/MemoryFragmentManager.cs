using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MemoryData {
	public GameObject memory;
	public AnimationClip memoryCutscene;
}

public class MemoryFragmentManager : MonoBehaviour {
	[SerializeField]
	GameObject memoryNexus;

	[SerializeField]
	List<MemoryData> memories = new List<MemoryData>();

	int currentMemory = 0;

	public delegate void PauseDelegate();
	public event PauseDelegate pauseEvent;
	public event PauseDelegate unpauseEvent;

	public static MemoryFragmentManager getMemoryFragmentManager() {
		return (MemoryFragmentManager) HushPuppy.safeFindComponent("GameController", "MemoryFragmentManager");
	}

	void Start() {
		InterrogationManager.getInterrogationManager().showMemoryEvent += showMemory;
		InterrogationManager.getInterrogationManager().getMemoryEvent += getMemory;

		for (int i = 0; i < memories.Count; i++) {
			memories[i].memory.SetActive(false);
		}
	}

	void showMemory() {
		memories[currentMemory].memory.SetActive(true);
	}

	void getMemory() {
		memories[currentMemory].memory.GetComponentInChildren<Animator>().SetTrigger("popup");
		if (pauseEvent != null) {
			pauseEvent();
		}
	}

	public void fullMemory() {
		memoryNexus.GetComponent<AnimationOverrider>().setAnimation(memories[currentMemory].memoryCutscene);
		
		//depois de setar a cutscene, precisa resetar o gameobject pra dar play na animação
		memoryNexus.SetActive(false);
		memoryNexus.SetActive(true);
	}

	public void endFullMemory() {
		if (unpauseEvent != null) {
			unpauseEvent();
		}
	}
}
