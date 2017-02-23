using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour {
	[SerializeField]
	List<ClueData> clues;
	[SerializeField]
	GameObject clueUI;

	int currentClue = 0;

	public static ClueManager getClueManager() {
		return (ClueManager) HushPuppy.safeFindComponent("GameController", "ClueManager");
	}
	
	void Start() {
		selectClue(clues[0]);
	}

	public void nextClue() {
		currentClue = (currentClue + 1) % clues.Count;
		selectClue(clues[currentClue]);
	}

	public void previousClue() {
		currentClue = (currentClue - 1 + clues.Count) % clues.Count;
		selectClue(clues[currentClue]);
	}

	void selectClue(ClueData clue) {
		clueUI.GetComponentsInChildren<Image>()[1].sprite = clue.sprite;
		clueUI.GetComponentInChildren<Text>().text = clue.title;
	}
}
