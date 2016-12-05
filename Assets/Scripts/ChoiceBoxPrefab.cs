using UnityEngine;
using UnityEngine.UI;

public class ChoiceBoxPrefab : MonoBehaviour {
	public delegate void selectDelegate(int index);
	public event selectDelegate selectEvent; 
	int index;
	string text;

	public void set(int index, string text) {
		this.index = index;
		this.text = text;
		displayText(text);
	}

	void displayText(string text) {
		this.GetComponentInChildren<Text>().text = text;
	}

	public void select() {
		selectEvent(index);
	}
}
