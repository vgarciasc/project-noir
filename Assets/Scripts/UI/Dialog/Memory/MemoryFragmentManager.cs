using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MemoryData {
	public string ID;
	public GameObject memory;
	public AnimationClip memoryCutscene;
}

public class MemoryFragmentManager : MonoBehaviour {
	[SerializeField]
	GameObject memoryNexus;

	[SerializeField]
	List<MemoryData> memories = new List<MemoryData>();

	int currentMemoryIndex = -1;

	[HideInInspector]
	public MemoryData currentMemory = null;

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
		currentMemoryIndex++;
		memories[currentMemoryIndex].memory.SetActive(true);
	}

	void getMemory() {
		memories[currentMemoryIndex].memory.GetComponentInChildren<Animator>().SetTrigger("popup");
		if (pauseEvent != null) {
			pauseEvent();
		}
	}

	public void fullMemory() {
		memoryNexus.GetComponent<AnimationOverrider>().setAnimation(memories[currentMemoryIndex].memoryCutscene);
		
		//depois de setar a cutscene, precisa resetar o gameobject pra dar play na animação
		memoryNexus.SetActive(false);
		memoryNexus.SetActive(true);
	}

	public void endFullMemory() {
		currentMemory = memories[currentMemoryIndex];

		if (unpauseEvent != null) {
			unpauseEvent();
		}
	}
}
