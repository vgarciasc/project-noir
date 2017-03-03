using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeEffectManager : MonoBehaviour {

	[SerializeField]
	Animator HypeEffect;

	InterrogationManager interrogation;

	void Start() {
		interrogation = InterrogationManager.getInterrogationManager();
		
		interrogation.startPressEvent += Press;
		interrogation.correctObjection += Objection;
		interrogation.wrongObjection += Objection;
	}

	void Press() {
		HypeEffect.SetTrigger("press");
	}

	void Objection() {
		HypeEffect.SetTrigger("objection");
	}
}
