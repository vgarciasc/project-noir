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

    public delegate void ObjectionDelegate();
    public event ObjectionDelegate startWrongObjection;
    public event ObjectionDelegate endWrongObjection;

	void Start () {
		interrogation = InterrogationManager.getInterrogationManager();
		mem_manager = MemoryFragmentManager.getMemoryFragmentManager();
		card_emitter = this.GetComponent<CardEmitter>();
		animator = this.GetComponent<Animator>();

		// GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().press_event += PressNow;
		interrogation.endPressEvent += endPress;
		interrogation.startPressEvent += startPress;
		interrogation.wrongObjection += wrongObjection;
		interrogation.correctObjection += correctObjection;
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
		animator.SetTrigger("press");
	}

	void AnimStopCurrentCard() {
		card_emitter.stopCurrentCard();
	}

	void AnimDoNothing() {
		//asdasdsfas
	}

	void startPress() {
		inPress = true;
		animator.SetTrigger("press");
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

	AnimatorStateInfo state;
	void wrongObjection() {
		// animator.SetLayerWeight(1, 0);
		// animator.SetLayerWeight(2, 1);

		// animator.SetFloat("speed_arg", 0);

		if (startWrongObjection != null) {
			startWrongObjection();
		}

		AnimStopCurrentCard();
		animator.SetTrigger("wrong_objection");
	}

	void previousAnimation() {
		// animator.SetLayerWeight(1, 1);
		// animator.SetLayerWeight(2, 0);

		if (endWrongObjection != null) {
			endWrongObjection();
		}

		// animator.SetFloat("speed_arg", 1);
		endPress();
		animator.SetTrigger("back_argument");
		interrogation.PreviousScene();
	}

	void correctObjection() {
		animator.SetTrigger("correct_objection");
	}
}
