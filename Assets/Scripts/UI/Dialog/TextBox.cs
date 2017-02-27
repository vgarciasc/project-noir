using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBox : MonoBehaviour {
	Text mainText;
	Coroutine typeText;
	bool shouldEndLine = false,
		textRunning = false;
	int currentCharacter;

	[Range(1, 10)]
	public int speed = 1;

	//DELEGATES
	public delegate void PortraitChangeDelegate(string command);
	public event PortraitChangeDelegate portraitChangeEvent;

	public delegate void VoidDelegate();
	public event VoidDelegate endTalkingEvent;

	public delegate void DialogueChangeSpeedDelegate(int newSpeed);
	public event DialogueChangeSpeedDelegate dialogueChangeSpeedEvent;

	void init() {
		mainText = this.GetComponentInChildren<Text>();
	}

	public void displayText(string target) {
		if (typeText != null) {
			StopCoroutine(typeText);
			typeText = null;
		}

		typeText = StartCoroutine(show(target));
	}

	IEnumerator show(string target) {
		init();

		currentCharacter = 0;
		string currentText = "";
		int totalCharacters = target.Length;

		char[] target_array = target.ToCharArray();
		string end_tags = "";
		bool openingBrackets = true;
		
		textRunning = true;

		while (currentCharacter	< totalCharacters) {
            currentText = target.Substring(0, currentCharacter);
			mainText.text = currentText + end_tags;

			yield return new WaitForSeconds(0.1f / speed);
			currentCharacter++;

			if (shouldEndLine) {
				mainText.text = target;
				break;
			}

			if (currentCharacter < totalCharacters &&
				target_array[currentCharacter - 1] == '<') {
				if (openingBrackets) {
					while (target_array[currentCharacter - 1] != '>') {
						currentCharacter++;
					}

					currentCharacter++;
					int auxIndex = currentCharacter - 1;
					while (target_array[auxIndex] != '<') {
						auxIndex++;
					}

					int end_tag_index = auxIndex;
					while (target_array[auxIndex] != '>') { 
						auxIndex++;
					}

					end_tags = target.Substring(end_tag_index, auxIndex - end_tag_index + 1);
					openingBrackets = false;
				}
				else {
					while (target_array[currentCharacter] != '>') {
						currentCharacter++;
					}

					currentCharacter++;
					end_tags = "";
					openingBrackets = true;
				}
			}

			target = parse(target);
			totalCharacters = target.Length;
			target_array = target.ToCharArray();
		}

		finishLine();
	}

	public void finishLine() {
		textRunning = false;
		shouldEndLine = false; //already ended
		if (endTalkingEvent != null) {
			endTalkingEvent();
		}
	}

	//to be used on input, its an 'order' to end the line
	public void endLine() {
		shouldEndLine = true;
	}

	public void closeText() {
		this.gameObject.SetActive(false);
	}

	int getTypingSpeed() {
		return -speed + 6;
	}

	string parse(string target) {
		char[] array = target.ToCharArray();
		int aux_index = currentCharacter;

		if (array[currentCharacter - 1] != '/') {
			//not a command
			return target;
		}

		for (; aux_index < target.Length && array[aux_index] != '/'; aux_index++);

		string command = target.Substring(currentCharacter, aux_index - currentCharacter);
		//example command: "#arjuna_normal"

		switch(command.ToCharArray()[0]) {
			//portrait change
			case '%':
				if (portraitChangeEvent != null) {
					portraitChangeEvent(command.Substring(1, command.Length - 1));
				}
				break;
			case '$':
				parseSpeed(command);
				break;
		}

		string aux = target.Remove(currentCharacter - 1, command.Length + 3);
		//+1: '[', +1: ']', +1: ' '

		return aux;
	}

	//example: /$speed_7/
	void parseSpeed(string command) {
		int index = command.IndexOf('_');

		string aux = command.Substring(index + 1, command.Length - index - 1);
		speed = System.Int16.Parse(aux);
	}
}
