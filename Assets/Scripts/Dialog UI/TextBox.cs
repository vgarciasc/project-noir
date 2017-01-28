using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBox : MonoBehaviour {
	Text mainText;
	Coroutine typeText;
	bool shouldEndLine = false,
		textRunning = false;

	[SerializeField]
	[Range(0, 6)]
	int speed = 1;
	[SerializeField]
	Animator idleArrow;

	void init() {
		mainText = this.GetComponentInChildren<Text>();
		toggleIdle(false);
	}

	public void displayText(string target) {
		typeText = StartCoroutine(show(target));
	}

	IEnumerator show(string target) {
		init();

		int currentCharacter = 0;
		string currentText = "";
		int totalCharacters = target.Length;

		char[] target_array = target.ToCharArray();
		string end_tags = "";
		bool openingBrackets = true;
		
		textRunning = true;

		while (currentCharacter	< totalCharacters) {
            currentText = target.Substring(0, currentCharacter);
			mainText.text = currentText + end_tags;
			Debug.Log(mainText.text + "___= " + currentCharacter);

			yield return HushPuppy.WaitUntilNFrames(getTypingSpeed());
			currentCharacter++;

			if (shouldEndLine) {
				mainText.text = target;
				break;
			}

			if (currentCharacter < totalCharacters &&
				target_array[currentCharacter] == '<') {
				if (openingBrackets) {
					while (target_array[currentCharacter] != '>') {
						currentCharacter++;
					}

					currentCharacter++;
					int auxIndex = currentCharacter;
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
		}

		textRunning = false;
		shouldEndLine = false; //already ended
		toggleIdle(true);
	}

	public void endLine() {
		shouldEndLine = true;
	}

	public void closeText() {
		this.gameObject.SetActive(false);
	}

	int getTypingSpeed() {
		return -speed + 6;
	}

	void toggleIdle(bool value) {
		if (idleArrow != null) {
			idleArrow.SetBool("idle", value);
		}
	}
}
