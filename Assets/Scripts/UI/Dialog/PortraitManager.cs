using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortraitManager : MonoBehaviour {

	PortraitDatabase portraitDatabase;
	[SerializeField]
	TextBox dialogue;

	[SerializeField]
	Image portraitLeft;
	[SerializeField]
	TextMeshProUGUI portraitLeftText;
	Coroutine portraitLeftTalking = null;
	PortraitData portraitLeftData;

	[SerializeField]
	Image portraitRight;
	[SerializeField]
	TextMeshProUGUI portraitRightText;
	Coroutine portraitRightTalking = null;
	PortraitData portraitRightData;

	public static PortraitManager getPortraitManager() {
		return (PortraitManager) HushPuppy.safeFindComponent("GameController", "PortraitManager");
	}

	void Start() {
		portraitDatabase = PortraitDatabase.getPortraitDatabase();
		dialogue.endTalkingEvent += stopTalking;
		dialogue.portraitChangeEvent += changePortrait;	
	}
	
	void changePortrait(string portrait) {
		//receiving "arjuna_normal"
		int index = 0;
		char[] array = portrait.ToCharArray();

		for (; index < portrait.Length && array[index] != '_'; index++);

		string character = portrait.Substring(0, index);
		string emote = portrait.Substring(index + 1, portrait.Length - index - 1);

		PortraitData newPortrait = portraitDatabase.getPortraitSprite(character, emote);

		if (character == "noir") {
			if (portraitRightTalking != null) {
				StopCoroutine(portraitRightTalking);
				portraitRightTalking = null;
			}			
			portraitRightData = newPortrait;
			portraitRightText.text = portraitDatabase.getPortraitTitle(character);
			portraitRightTalking = StartCoroutine(moveLips(portraitRight, newPortrait));
		}
		else {
			if (portraitLeftTalking != null) {
				StopCoroutine(portraitLeftTalking);
				portraitLeftTalking = null;
			}
			portraitLeftData = newPortrait;
			portraitLeftText.text = portraitDatabase.getPortraitTitle(character);
			portraitLeftTalking = StartCoroutine(moveLips(portraitLeft, newPortrait));
		}
	}

	IEnumerator moveLips(Image image, PortraitData portraitData) {
		int aux = 0;
		while (true) {
			if (aux % 2 == 0) {
				image.sprite = portraitData.spriteMouthClosed;
			}
			else {
				image.sprite = portraitData.spriteMouthOpen;
			}
			aux++;
			float time = Random.Range(0.2f, 0.4f);
			time /= dialogue.speed;
			yield return new WaitForSeconds(time);
		}
	}

	public void stopTalking() {
		if (portraitLeftTalking != null) {
			StopCoroutine(portraitLeftTalking);
			portraitLeftTalking = null;
			portraitLeft.sprite = portraitLeftData.spriteMouthClosed;
		}

		if (portraitRightTalking != null) {
			StopCoroutine(portraitRightTalking);
			portraitRightTalking = null;
			portraitRight.sprite = portraitRightData.spriteMouthClosed;
		}	
	}
}
