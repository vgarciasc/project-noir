using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChoiceBox : MonoBehaviour {
	public delegate void selectChoiceDelegate(int index);
	public event selectChoiceDelegate selectChoiceEvent;

	[SerializeField]
	GameObject choicePrefab;
	[SerializeField]
	Transform choicePrefabContainer;

	public void displayChoices(List<string> targets) {
		for (int i = 0; i < targets.Count; i++) {
			GameObject aux = (GameObject) Instantiate(choicePrefab, choicePrefabContainer, false);
			ChoiceBoxPrefab cbp = aux.GetComponent<ChoiceBoxPrefab>();
			cbp.set(i, targets[i]);
			cbp.selectEvent += selectChoice;
		}
	}

	public void clearChoices() {
		HushPuppy.destroyChildren(choicePrefabContainer.gameObject);
	}

	void selectChoice(int index) {
		selectChoiceEvent(index);
	}
}
