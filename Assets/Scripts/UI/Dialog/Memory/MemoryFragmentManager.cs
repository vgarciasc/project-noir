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

	int currentMemoryIndex = 0;

	[HideInInspector]
	public MemoryData currentMemory = null;

	public delegate void PauseDelegate();
	public event PauseDelegate pauseEvent;
	public event PauseDelegate unpauseEvent;

	InterrogationManager interrogation;

	public static MemoryFragmentManager getMemoryFragmentManager() {
		return (MemoryFragmentManager) HushPuppy.safeFindComponent("GameController", "MemoryFragmentManager");
	}

	void Start() {
		interrogation = InterrogationManager.getInterrogationManager();

		interrogation.showMemoryEvent += showMemory;
		interrogation.getMemoryEvent += getMemory;
		interrogation.wrongObjection += disappearMemory;
		interrogation.startPressEvent += disappearMemory;
	}

	void showMemory() {
		memories[currentMemoryIndex].memory.GetComponent<Animator>().SetTrigger("start");
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
		currentMemoryIndex++;

		if (unpauseEvent != null) {
			unpauseEvent();
		}
	}

	void disappearMemory() {
		if (currentMemoryIndex < memories.Count) {
			memories[currentMemoryIndex].memory.GetComponentInChildren<Animator>().SetTrigger("disappear");
		}
	}
}
