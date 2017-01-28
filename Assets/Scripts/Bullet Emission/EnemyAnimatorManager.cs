using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEmissionData {
	public CardPatternData card;
	public Transform emitter;
}

public class EnemyAnimatorManager : MonoBehaviour {
	[SerializeField]
	GameObject pressSign;
	[SerializeField]
	GameObject weakPointSign;

	[SerializeField]
	List<CardEmissionData> cards;

	CardEmitter card_emitter;
	InterrogationManager interrogation;

	bool inPress = false;
	bool inWeakPoint = false;

	void Start () {
		card_emitter = this.GetComponent<CardEmitter>();
		interrogation = InterrogationManager.getInterrogationManager();

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().press_event += PressNow;
	}

	void Update() {
		pressSign.SetActive(!inPress);
		weakPointSign.SetActive(inWeakPoint);
	}
	
	void AnimPlayCard(int index) {
		card_emitter.PlayCard(cards[index].card, cards[index].emitter);
	}

	void CallNextLine() {
		interrogation.Next();
	}

	void PressNow() {
		if (!inPress) {
			this.GetComponent<Animator>().SetTrigger("press");
			interrogation.Press();
		}
		else if (inWeakPoint) {
			this.GetComponent<Animator>().SetTrigger("weakpoint");
		}
	}

	void AnimStopCurrentCard() {
		card_emitter.stopCurrentCard();
	}

	void AnimDoNothing() {
		//asdasdsfas
	}

	void AnimInPress(int value) {
		inPress = value != 0;
	}

	void AnimInWeakPoint(int value) {
		inWeakPoint = value != 0;
	}
}
