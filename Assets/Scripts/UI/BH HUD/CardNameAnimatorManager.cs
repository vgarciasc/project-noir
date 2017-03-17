using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardNameAnimatorManager : MonoBehaviour {

	Animator animator;
	public TextMeshProUGUI text_1, text_2, text_3;

	void init() {
		animator = this.GetComponentInChildren<Animator>();
	}

	public void showCard(string text1, string text2, string text3) {
		init();

		text_1.text = text1;
		text_2.text = text2;
		text_3.text = text3;
		animator.SetTrigger("show");
	}
}
