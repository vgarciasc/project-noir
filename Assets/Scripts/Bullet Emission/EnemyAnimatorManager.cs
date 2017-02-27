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
	MemoryFragmentManager mem_manager;
	Animator animator;

	bool inPress = false;
	bool inWeakPoint = false;

	void Start () {
		interrogation = InterrogationManager.getInterrogationManager();
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		card_emitter = this.GetComponent<CardEmitter>();
		animator = this.GetComponent<Animator>();

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().press_event += PressNow;
		interrogation.endPressEvent += endPress;
		interrogation.startPressEvent += startPress;
		mem_manager.pauseEvent += pauseAnimation;
		mem_manager.unpauseEvent += unpauseAnimation;
	}
	
	void AnimPlayCard(int index) {
		if (cards.Count <= index) {
			return;
		}
		
		for (int i = 0; i < cards[index].emitter.Count; i++) {
			card_emitter.PlayCard(cards[index].card, cards[index].emitter[i], index);
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
			animator.SetTrigger("press");
			interrogation.Press();
		}
	}

	void AnimStopCurrentCard() {
		card_emitter.stopCurrentCard();
	}

	void AnimDoNothing() {
		//asdasdsfas
	}

	void startPress() {
		inPress = true;
	}

	void endPress() {
		inPress = false;
	}

	void pauseAnimation() {
		animator.speed = 0;
		// animator.SetTrigger("pause");
		// AnimStopCurrentCard();
	}

	void unpauseAnimation() {
		animator.speed = 1;
		// animator.SetTrigger("restart");
	}
}
