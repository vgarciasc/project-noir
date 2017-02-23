using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEmissionData {
	public string id;
	public CardPatternData card;
	public List<Transform> emitter;
}

[System.Serializable]
public class AnimationEventData {
	public string id;
	public List<GameObject> activateThese;
	public string triggerToSet;
}

public class EnemyAnimatorManager : MonoBehaviour {
	[SerializeField]
	List<CardEmissionData> cards;
	[SerializeField]
	List<AnimationEventData> events;

	CardEmitter card_emitter;
	InterrogationManager interrogation;

	bool inPress = false;
	bool inWeakPoint = false;

	void Start () {
		card_emitter = this.GetComponent<CardEmitter>();
		interrogation = InterrogationManager.getInterrogationManager();

		// GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().press_event += PressNow;
	}
	
	void AnimPlayCard(int index) {
		if (cards.Count <= index) {
			return;
		}
		
		for (int i = 0; i < cards[index].emitter.Count; i++) {
			card_emitter.PlayCard(cards[index].card, cards[index].emitter[i]);
		}
	}

	void AnimPlayEvent(int index) {
		for (int i = 0; i < events[index].activateThese.Count; i++) {
			events[index].activateThese[i].SetActive(true);
			if (events[index].activateThese[i].GetComponentInChildren<Animator>() != null &&
				events[index].triggerToSet.Length != 0) {
				events[index].activateThese[i].GetComponentInChildren<Animator>().SetTrigger(
					events[index].triggerToSet
				);
			}
		}
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
