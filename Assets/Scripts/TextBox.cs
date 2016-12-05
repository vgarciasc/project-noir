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
	int speed;
	[SerializeField]
	Animator idleArrow;

	public void displayText(string target) {
		typeText = StartCoroutine(show(target));
	}

	IEnumerator show(string target) {
		init();

		int currentCharacter = 0;
		string currentText = "";
		int totalCharacters = target.Length;
		
		textRunning = true;

		while (currentCharacter	< totalCharacters) {
            currentText = target.Substring(0, currentCharacter);
			mainText.text = currentText;

			yield return HushPuppy.WaitUntilNFrames(getTypingSpeed());
			currentCharacter++;

			if (shouldEndLine) {
				mainText.text = target;
				break; }
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
		idleArrow.SetBool("idle", value);
	}

	void init() {
		mainText = this.GetComponentInChildren<Text>();
		toggleIdle(false);
	}
}
