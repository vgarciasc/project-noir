using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInterrogationComponent : MonoBehaviour {

	InterrogationManager interrogation;

	void Start () {
		interrogation = InterrogationManager.getInterrogationManager();
	}
	
	void CallNextLine() {
		interrogation.Next();
	}
}
