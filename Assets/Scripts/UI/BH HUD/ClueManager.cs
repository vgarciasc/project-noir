using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueManager : MonoBehaviour {
	[SerializeField]
	List<ClueData> cluesInPossession;
	[SerializeField]
	List<ClueData> possibleClues;
	[SerializeField]
	GameObject clueUI;

	int currentClueIndex = 0;

	[HideInInspector]
	public ClueData currentClue = null;

	MemoryFragmentManager mem_manager;

	public static ClueManager getClueManager() {
		return (ClueManager) HushPuppy.safeFindComponent("GameController", "ClueManager");
	}
	
	void Start() {
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		mem_manager.unpauseEvent += getNewMemoryClue;

		selectClue(cluesInPossession[0]);
	}

	public void nextClue() {
		currentClueIndex = (currentClueIndex + 1) % cluesInPossession.Count;
		selectClue(cluesInPossession[currentClueIndex]);
	}

	public void previousClue() {
		currentClueIndex = (currentClueIndex - 1 + cluesInPossession.Count) % cluesInPossession.Count;
		selectClue(cluesInPossession[currentClueIndex]);
	}

	void selectClue(ClueData clue) {
		clueUI.GetComponentsInChildren<Image>()[1].sprite = clue.sprite;
		clueUI.GetComponentInChildren<TextMeshProUGUI>().text = clue.title;

		currentClue = clue;
	}

	void getNewMemoryClue() {
		for (int i = 0; i < possibleClues.Count; i++) {
			if (possibleClues[i].ID == mem_manager.currentMemory.ID) {
				cluesInPossession.Add(possibleClues[i]);
				return;
			}
		}

		Debug.Log("Memory was captured, but there was no clue with ID '" + mem_manager.currentMemory.ID + "'.");
		Debug.Break();
	}
}
